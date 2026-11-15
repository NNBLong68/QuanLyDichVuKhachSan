using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using QuanLyDichVuKhachSan.Helpers;
using QuanLyDichVuKhachSan.Models;

namespace QuanLyDichVuKhachSan.Services
{
    public class InventoryExcelImportResult
    {
        public string SupplierName { get; set; } = string.Empty;
        public string ReceiptNote { get; set; } = string.Empty;
        public List<WarehouseReceiptDraftItem> Lines { get; } = new();
        public List<string> Errors { get; } = new();
        public decimal TotalCost => Lines.Sum(x => x.TotalCost);
    }

    /// <summary>
    /// Tạo file Excel mẫu, đọc file Excel nhập kho và xuất lại phiếu nhập từ CSDL.
    /// </summary>
    public static class InventoryExcelService
    {
        private const string MainSheetName = "PhieuNhapKho";
        private const string CatalogSheetName = "DanhMuc";
        private const string GuideSheetName = "HuongDan";
        private const string CatalogRangeName = "DanhMucMaHang";
        private const int TemplateRows = 200;
        private const int HeaderRow = 7;
        private const int DataStartRow = 8;

        public static string ArchiveDirectory => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "QuanLyDichVuKhachSan",
            "PhieuNhapKho");

        #region Tạo file mẫu

        public static void CreateTemplate(string filePath, IReadOnlyList<FoodItem> foodItems)
        {
            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add(MainSheetName);

            BuildReceiptSheet(workbook, sheet, null, null, foodItems, null, isTemplate: true);
            BuildCatalogSheet(workbook, foodItems);
            BuildGuideSheet(workbook);

            try { sheet.SetTabColor(XLColor.FromHtml("#0EA5E9")); } catch { }
            workbook.SaveAs(filePath);
        }

        #endregion

        #region Đọc file Excel

        public static InventoryExcelImportResult ReadImportFile(string filePath, IReadOnlyList<FoodItem> foodItems)
        {
            var result = new InventoryExcelImportResult();

            using var workbook = new XLWorkbook(filePath);
            var sheet = workbook.Worksheets.FirstOrDefault(x =>
                            string.Equals(x.Name, MainSheetName, StringComparison.OrdinalIgnoreCase))
                        ?? workbook.Worksheets.First();

            result.SupplierName = FindLabelValue(sheet, "Nhà cung cấp");
            result.ReceiptNote = FindLabelValue(sheet, "Ghi chú phiếu");

            if (string.IsNullOrWhiteSpace(result.SupplierName))
                result.Errors.Add("Chưa nhập tên Nhà cung cấp ở ô B2 của file Excel.");

            if (result.SupplierName.Trim().Length > 100)
                result.Errors.Add("Tên Nhà cung cấp vượt quá 100 ký tự.");

            int headerRow = FindHeaderRow(sheet);
            if (headerRow <= 0)
            {
                result.Errors.Add("Không tìm thấy dòng tiêu đề 'Mã mặt hàng' trong file Excel.");
                return result;
            }

            int codeCol = FindColumnByHeader(sheet, headerRow, "Mã mặt hàng", "Mã hàng");
            int nameCol = FindColumnByHeader(sheet, headerRow, "Tên mặt hàng", "Tên hàng");
            int qtyCol = FindColumnByHeader(sheet, headerRow, "Số lượng nhập", "Số lượng");
            int priceCol = FindColumnByHeader(sheet, headerRow, "Đơn giá nhập", "Đơn giá");
            int batchCol = FindColumnByHeader(sheet, headerRow, "Số lô");
            int expiryCol = FindColumnByHeader(sheet, headerRow, "Hạn sử dụng", "HSD");
            int noteCol = FindColumnByHeader(sheet, headerRow, "Ghi chú");

            if (codeCol <= 0 && nameCol <= 0)
            {
                result.Errors.Add("File Excel thiếu cột 'Mã mặt hàng' hoặc 'Tên mặt hàng'.");
                return result;
            }

            if (qtyCol <= 0)
            {
                result.Errors.Add("File Excel thiếu cột 'Số lượng nhập'.");
                return result;
            }

            if (priceCol <= 0)
            {
                result.Errors.Add("File Excel thiếu cột 'Đơn giá nhập'.");
                return result;
            }

            int lastRow = sheet.LastRowUsed()?.RowNumber() ?? headerRow;
            int lineIndex = 0;

            for (int row = headerRow + 1; row <= lastRow; row++)
            {
                string rawCode = codeCol > 0 ? GetCellText(sheet.Cell(row, codeCol)) : string.Empty;
                string rawName = nameCol > 0 ? GetCellText(sheet.Cell(row, nameCol)) : string.Empty;

                if (string.IsNullOrWhiteSpace(rawCode) && string.IsNullOrWhiteSpace(rawName))
                    continue;

                FoodItem? foodItem = ResolveFoodItem(rawCode, rawName, foodItems);
                if (foodItem == null)
                {
                    string hint = !string.IsNullOrWhiteSpace(rawCode) ? rawCode : rawName;
                    result.Errors.Add($"Dòng {row}: Không tìm thấy mặt hàng '{hint}' trong danh mục kho.");
                    continue;
                }

                if (!TryReadInt(sheet.Cell(row, qtyCol), out int quantity) || quantity <= 0)
                {
                    result.Errors.Add($"Dòng {row}: Số lượng nhập phải là số nguyên lớn hơn 0.");
                    continue;
                }

                if (!TryReadDecimal(sheet.Cell(row, priceCol), out decimal importPrice) || importPrice < 0)
                {
                    result.Errors.Add($"Dòng {row}: Đơn giá nhập không hợp lệ (phải >= 0).");
                    continue;
                }

                DateTime? expiryDate = null;
                if (expiryCol > 0)
                {
                    var expiryCell = sheet.Cell(row, expiryCol);
                    if (!expiryCell.IsEmpty())
                    {
                        if (!TryReadDate(expiryCell, out DateTime parsedExpiry))
                        {
                            result.Errors.Add($"Dòng {row}: Hạn sử dụng không đúng định dạng dd/MM/yyyy.");
                            continue;
                        }

                        if (parsedExpiry.Date <= DateTime.Today)
                        {
                            result.Errors.Add($"Dòng {row}: Hạn sử dụng phải sau ngày hôm nay.");
                            continue;
                        }

                        expiryDate = parsedExpiry.Date;
                    }
                }

                string batchNumber = batchCol > 0 ? GetCellText(sheet.Cell(row, batchCol)).Trim() : string.Empty;
                if (batchNumber.Length > 50)
                    batchNumber = batchNumber[..50];

                if (string.IsNullOrWhiteSpace(batchNumber))
                {
                    batchNumber = $"LO-{DateTime.Now:MMdd}-{(char)('A' + (lineIndex % 26))}";
                }

                string lineNote = noteCol > 0 ? GetCellText(sheet.Cell(row, noteCol)).Trim() : string.Empty;
                if (lineNote.Length > 255)
                    lineNote = lineNote[..255];

                result.Lines.Add(new WarehouseReceiptDraftItem
                {
                    FoodItemId = foodItem.Id,
                    FoodItemName = foodItem.Name,
                    RetailUnit = foodItem.RetailUnit,
                    ImportUnit = string.IsNullOrWhiteSpace(foodItem.DefaultImportUnit) ? "Thùng" : foodItem.DefaultImportUnit,
                    ConversionRate = foodItem.DefaultConversionRate > 0 ? foodItem.DefaultConversionRate : 1,
                    Quantity = quantity,
                    ImportPrice = Math.Round(importPrice, 2),
                    BatchNumber = batchNumber,
                    ExpiryDate = expiryDate,
                    Note = lineNote
                });

                lineIndex++;
            }

            if (result.Lines.Count == 0 && result.Errors.Count == 0)
                result.Errors.Add("File Excel không có dòng mặt hàng nào để nhập kho.");

            return result;
        }

        #endregion

        #region Lưu trữ & xuất lại file

        public static string CopyToArchive(string sourceFilePath, string receiptCode)
        {
            Directory.CreateDirectory(ArchiveDirectory);

            string safeCode = string.Concat(receiptCode.Select(c =>
                Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));

            string fileName = $"{safeCode}.xlsx";
            string destination = Path.Combine(ArchiveDirectory, fileName);

            File.Copy(sourceFilePath, destination, overwrite: true);

            return Path.Combine("PhieuNhapKho", fileName);
        }

        public static string GetAbsoluteArchivePath(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return string.Empty;

            if (Path.IsPathRooted(relativePath))
                return relativePath;

            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "QuanLyDichVuKhachSan",
                relativePath);
        }

        public static void ExportReceipt(string destinationPath, InventoryReceipt receipt,
            IReadOnlyList<InventoryBatch> lines, IReadOnlyList<FoodItem> foodItems)
        {
            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add(MainSheetName);

            BuildReceiptSheet(workbook, sheet, receipt, lines, foodItems, null, isTemplate: false);
            BuildCatalogSheet(workbook, foodItems);

            string? folder = Path.GetDirectoryName(destinationPath);
            if (!string.IsNullOrWhiteSpace(folder))
                Directory.CreateDirectory(folder);

            if (File.Exists(destinationPath))
                File.Delete(destinationPath);

            workbook.SaveAs(destinationPath);
        }

        #endregion

        #region Dựng sheet phiếu nhập

        private static void BuildReceiptSheet(XLWorkbook workbook, IXLWorksheet sheet,
            InventoryReceipt? receipt, IReadOnlyList<InventoryBatch>? lines,
            IReadOnlyList<FoodItem> foodItems, IReadOnlyList<Supplier>? suppliers, bool isTemplate)
        {
            sheet.ShowGridLines = false;

            // Tiêu đề
            var titleRange = sheet.Range("A1:J1");
            titleRange.Merge();
            titleRange.Value = "PHIẾU NHẬP KHO HÀNG";
            titleRange.Style.Font.Bold = true;
            titleRange.Style.Font.FontSize = 16;
            titleRange.Style.Font.FontColor = XLColor.FromHtml("#0F172A");
            titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            titleRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            sheet.Row(1).Height = 30;

            // Thông tin phiếu
            SetLabel(sheet.Cell("A2"), "Nhà cung cấp:");
            SetValueRange(sheet.Range("B2:D2"), isTemplate ? string.Empty : receipt?.Supplier ?? string.Empty, editable: isTemplate);
            SetLabel(sheet.Cell("E2"), "Mã phiếu:");
            SetValueRange(sheet.Range("F2:G2"), isTemplate ? "Tự động khi nhập" : receipt?.ReceiptCode ?? string.Empty, editable: false);

            SetLabel(sheet.Cell("A3"), "Ghi chú phiếu:");
            SetValueRange(sheet.Range("B3:E3"), isTemplate ? string.Empty : receipt?.Note ?? string.Empty, editable: isTemplate);
            SetLabel(sheet.Cell("F3"), "Ngày nhập:");
            SetValueRange(sheet.Range("G3:J3"),
                isTemplate ? "Tự động khi nhập" : receipt?.ImportDate.ToString("dd/MM/yyyy HH:mm") ?? string.Empty,
                editable: false);

            SetLabel(sheet.Cell("A4"), "Người nhập:");
            SetValueRange(sheet.Range("B4:D4"),
                isTemplate ? "Tự động" : receipt?.ImportedBy ?? string.Empty,
                editable: false);
            SetLabel(sheet.Cell("E4"), "Tổng tiền:");
            SetValueRange(sheet.Range("F4:G4"),
                isTemplate ? string.Empty : receipt?.TotalCostDisplay ?? string.Empty,
                editable: false);

            if (isTemplate)
            {
                var guideRange = sheet.Range("A6:J6");
                guideRange.Merge();
                guideRange.Value = "Điền tên nhà cung cấp, ghi chú phiếu và các dòng mặt hàng vào ô màu vàng. " +
                                   "Cột Tên mặt hàng / ĐVT / Hệ số tự động tra cứu từ Mã mặt hàng.";
                guideRange.Style.Font.Italic = true;
                guideRange.Style.Font.FontColor = XLColor.FromHtml("#64748B");
                guideRange.Style.Alignment.WrapText = true;
                sheet.Row(6).Height = 30;
            }

            // Tiêu đề bảng
            string[] headers =
            {
                "STT", "Mã mặt hàng (*)", "Tên mặt hàng", "ĐVT nhập", "Hệ số quy đổi",
                "Số lượng nhập (*)", "Đơn giá nhập (*)", "Số lô", "Hạn sử dụng", "Ghi chú"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                var cell = sheet.Cell(HeaderRow, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontColor = XLColor.FromHtml("#1E293B");
                cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#E2E8F0");
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                cell.Style.Alignment.WrapText = true;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }

            sheet.Row(HeaderRow).Height = 34;

            int dataEndRow = isTemplate
                ? DataStartRow + TemplateRows - 1
                : Math.Max(DataStartRow, DataStartRow + (lines?.Count ?? 0) - 1);

            if (isTemplate)
            {
                BuildTemplateRows(workbook, sheet, foodItems, dataEndRow);
            }
            else
            {
                BuildExportRows(sheet, lines ?? Array.Empty<InventoryBatch>(), foodItems, dataEndRow);
            }

            // Định dạng bảng
            var tableRange = sheet.Range(HeaderRow, 1, dataEndRow, headers.Length);
            tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Hair;
            tableRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            sheet.Range(DataStartRow, 1, dataEndRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            sheet.Range(DataStartRow, 2, dataEndRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            sheet.Range(DataStartRow, 4, dataEndRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            sheet.Range(DataStartRow, 8, dataEndRow, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            sheet.Range(DataStartRow, 6, dataEndRow, 6).Style.NumberFormat.Format = "0";
            sheet.Range(DataStartRow, 7, dataEndRow, 7).Style.NumberFormat.Format = "#,##0";
            sheet.Range(DataStartRow, 9, dataEndRow, 9).Style.NumberFormat.Format = "dd/mm/yyyy";

            // Độ rộng cột
            sheet.Column(1).Width = 6;
            sheet.Column(2).Width = 16;
            sheet.Column(3).Width = 30;
            sheet.Column(4).Width = 12;
            sheet.Column(5).Width = 13;
            sheet.Column(6).Width = 13;
            sheet.Column(7).Width = 15;
            sheet.Column(8).Width = 15;
            sheet.Column(9).Width = 14;
            sheet.Column(10).Width = 26;

            sheet.SheetView.FreezeRows(HeaderRow);
        }

        private static void BuildTemplateRows(XLWorkbook workbook, IXLWorksheet sheet,
            IReadOnlyList<FoodItem> foodItems, int dataEndRow)
        {
            for (int row = DataStartRow; row <= dataEndRow; row++)
            {
                sheet.Cell(row, 1).FormulaA1 = $"IF($B{row}=\"\",\"\",ROW()-{HeaderRow})";

                sheet.Cell(row, 3).FormulaA1 =
                    $"IF($B{row}=\"\",\"\",IFERROR(VLOOKUP($B{row},{CatalogSheetName}!$A$2:$D$1000,2,FALSE),\"\"))";
                sheet.Cell(row, 4).FormulaA1 =
                    $"IF($B{row}=\"\",\"\",IFERROR(VLOOKUP($B{row},{CatalogSheetName}!$A$2:$D$1000,3,FALSE),\"\"))";
                sheet.Cell(row, 5).FormulaA1 =
                    $"IF($B{row}=\"\",\"\",IFERROR(VLOOKUP($B{row},{CatalogSheetName}!$A$2:$D$1000,4,FALSE),\"\"))";

                // Ô nhập liệu có nền vàng nhạt
                foreach (int col in new[] { 2, 6, 7, 8, 9, 10 })
                {
                    sheet.Cell(row, col).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFF7D6");
                }

                foreach (int col in new[] { 1, 3, 4, 5 })
                {
                    sheet.Cell(row, col).Style.Fill.BackgroundColor = XLColor.FromHtml("#F8FAFC");
                }
            }

            if (foodItems.Count > 0)
            {
                var validation = sheet.Range(DataStartRow, 2, dataEndRow, 2).CreateDataValidation();
                validation.List($"={CatalogRangeName}");
                // Cho phép gõ tay tên/mã ngoài danh sách gợi ý (app sẽ tự dò theo mã hoặc tên).
                validation.ShowErrorMessage = false;
                validation.InCellDropdown = true;
            }
        }

        private static void BuildExportRows(IXLWorksheet sheet,
            IReadOnlyList<InventoryBatch> lines, IReadOnlyList<FoodItem> foodItems, int dataEndRow)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                int row = DataStartRow + i;
                var food = foodItems.FirstOrDefault(x => x.Id == line.FoodItemId);

                sheet.Cell(row, 1).Value = i + 1;
                sheet.Cell(row, 2).Value = food?.ItemCode ?? $"MH{line.FoodItemId:D3}";
                sheet.Cell(row, 3).Value = line.FoodItemName;
                sheet.Cell(row, 4).Value = line.ImportUnit;
                sheet.Cell(row, 5).Value = line.ConversionRate;
                sheet.Cell(row, 6).Value = line.Quantity;
                sheet.Cell(row, 7).Value = line.ImportPrice;
                sheet.Cell(row, 8).Value = line.BatchNumber;
                if (line.ExpiryDate.HasValue)
                    sheet.Cell(row, 9).Value = line.ExpiryDate.Value.Date;
                sheet.Cell(row, 10).Value = line.Note;
            }

            // Nếu phiếu không có dòng chi tiết, vẫn giữ vùng bảng trống gọn gàng
            if (lines.Count == 0)
            {
                for (int col = 1; col <= 10; col++)
                    sheet.Cell(DataStartRow, col).Style.Fill.BackgroundColor = XLColor.FromHtml("#F8FAFC");
            }
        }

        #endregion

        #region Sheet danh mục & hướng dẫn

        private static void BuildCatalogSheet(XLWorkbook workbook, IReadOnlyList<FoodItem> foodItems)
        {
            var catalog = workbook.Worksheets.Add(CatalogSheetName);

            catalog.Cell(1, 1).Value = "Mã mặt hàng";
            catalog.Cell(1, 2).Value = "Tên mặt hàng";
            catalog.Cell(1, 3).Value = "ĐVT nhập";
            catalog.Cell(1, 4).Value = "Hệ số quy đổi";

            var headerRange = catalog.Range(1, 1, 1, 4);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#E2E8F0");

            int row = 2;
            foreach (var item in foodItems)
            {
                catalog.Cell(row, 1).Value = item.ItemCode;
                catalog.Cell(row, 2).Value = item.Name;
                catalog.Cell(row, 3).Value = item.DefaultImportUnit;
                catalog.Cell(row, 4).Value = item.DefaultConversionRate;
                row++;
            }

            catalog.Column(1).Width = 16;
            catalog.Column(2).Width = 34;
            catalog.Column(3).Width = 14;
            catalog.Column(4).Width = 16;

            if (foodItems.Count > 0)
            {
                var codeRange = catalog.Range(2, 1, foodItems.Count + 1, 1);
                workbook.DefinedNames.Add(CatalogRangeName, codeRange);
            }

            try { catalog.Hide(); } catch { }
        }

        private static void BuildGuideSheet(XLWorkbook workbook)
        {
            var guide = workbook.Worksheets.Add(GuideSheetName);
            guide.ShowGridLines = false;
            guide.Column(1).Width = 110;

            string[] lines =
            {
                "HƯỚNG DẪN NHẬP KHO BẰNG EXCEL",
                "",
                "1. Điền tên nhà cung cấp vào ô B2 của sheet PhieuNhapKho.",
                "2. Điền ghi chú phiếu vào ô B3 (không bắt buộc).",
                "3. Điền các dòng mặt hàng bắt đầu từ dòng 8.",
                "4. Cột Mã mặt hàng có dropdown danh mục. Có thể gõ tên mặt hàng nếu không nhớ mã.",
                "5. Cột Tên mặt hàng / ĐVT nhập / Hệ số quy đổi tự động tra cứu, không cần sửa.",
                "6. Số lượng nhập phải là số nguyên lớn hơn 0. Đơn giá nhập phải >= 0.",
                "7. Hạn sử dụng định dạng dd/MM/yyyy. Để trống nếu lô hàng không có HSD.",
                "8. Mã phiếu, ngày nhập và người nhập do app tự sinh khi nhập kho.",
                "9. Không đổi tên sheet PhieuNhapKho. Chỉ hỗ trợ file .xlsx.",
                "",
                "LƯU Ý: Nếu mã mặt hàng không có trong danh mục, dòng đó sẽ bị báo lỗi và toàn bộ file chưa được nhập."
            };

            for (int i = 0; i < lines.Length; i++)
            {
                var cell = guide.Cell(i + 1, 1);
                cell.Value = lines[i];
                cell.Style.Alignment.WrapText = true;
                if (i == 0)
                {
                    cell.Style.Font.Bold = true;
                    cell.Style.Font.FontSize = 14;
                    cell.Style.Font.FontColor = XLColor.FromHtml("#0F172A");
                }
                else
                {
                    cell.Style.Font.FontColor = XLColor.FromHtml("#334155");
                }
            }

            try { guide.SetTabColor(XLColor.FromHtml("#10B981")); } catch { }
        }

        #endregion

        #region Helper đọc dữ liệu

        private static void SetLabel(IXLCell cell, string text)
        {
            cell.Value = text;
            cell.Style.Font.Bold = true;
            cell.Style.Font.FontColor = XLColor.FromHtml("#475569");
            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }

        private static void SetValueRange(IXLRange range, string value, bool editable)
        {
            range.Merge();
            range.Value = value;
            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.OutsideBorderColor = XLColor.FromHtml("#CBD5E1");
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            range.Style.Alignment.WrapText = true;

            if (editable)
                range.Style.Fill.BackgroundColor = XLColor.FromHtml("#FFF7D6");
            else
                range.Style.Fill.BackgroundColor = XLColor.FromHtml("#F8FAFC");
        }

        private static int FindHeaderRow(IXLWorksheet sheet)
        {
            int lastRow = sheet.LastRowUsed()?.RowNumber() ?? 0;
            int scanTo = Math.Min(lastRow, 80);

            for (int row = 1; row <= scanTo; row++)
            {
                bool hasCodeHeader = false;
                bool hasQuantityHeader = false;

                for (int col = 1; col <= 15; col++)
                {
                    string text = GetCellText(sheet.Cell(row, col));
                    if (string.IsNullOrWhiteSpace(text)) continue;

                    string key = TextSearchHelper.ToSearchKey(text);
                    if (key.StartsWith("ma mat hang", StringComparison.Ordinal) || key == "ma hang")
                        hasCodeHeader = true;
                    if (key.StartsWith("so luong nhap", StringComparison.Ordinal) || key == "so luong")
                        hasQuantityHeader = true;
                }

                if (hasCodeHeader && hasQuantityHeader)
                    return row;
            }

            return 0;
        }

        private static string FindLabelValue(IXLWorksheet sheet, string label)
        {
            string labelKey = TextSearchHelper.ToSearchKey(label);
            int lastRow = sheet.LastRowUsed()?.RowNumber() ?? 0;
            int scanTo = Math.Min(lastRow, 30);

            for (int row = 1; row <= scanTo; row++)
            {
                for (int col = 1; col <= 10; col++)
                {
                    string text = GetCellText(sheet.Cell(row, col));
                    if (string.IsNullOrWhiteSpace(text)) continue;

                    string key = TextSearchHelper.ToSearchKey(text);
                    if (!key.Contains(labelKey)) continue;

                    // Trường hợp giá trị nằm cùng ô sau dấu hai chấm
                    int colonIndex = text.IndexOf(':');
                    if (colonIndex >= 0 && colonIndex < text.Length - 1)
                    {
                        string inlineValue = text[(colonIndex + 1)..].Trim();
                        if (!string.IsNullOrWhiteSpace(inlineValue) &&
                            !string.Equals(inlineValue, label, StringComparison.OrdinalIgnoreCase))
                            return inlineValue;
                    }

                    // Giá trị nằm ở ô bên phải (ô đầu tiên sau nhãn).
                    int valueCol = col + 1;
                    if (valueCol <= 15)
                    {
                        string value = GetCellText(sheet.Cell(row, valueCol));
                        return value;
                    }

                    return string.Empty;
                }
            }

            return string.Empty;
        }

        private static int FindColumnByHeader(IXLWorksheet sheet, int headerRow, params string[] candidates)
        {
            for (int col = 1; col <= 25; col++)
            {
                string text = GetCellText(sheet.Cell(headerRow, col));
                if (string.IsNullOrWhiteSpace(text)) continue;

                string key = TextSearchHelper.ToSearchKey(text);
                foreach (var candidate in candidates)
                {
                    string candidateKey = TextSearchHelper.ToSearchKey(candidate);
                    if (key == candidateKey || key.Contains(candidateKey))
                        return col;
                }
            }

            return 0;
        }

        private static FoodItem? ResolveFoodItem(string rawCode, string rawName, IReadOnlyList<FoodItem> foodItems)
        {
            string code = rawCode.Trim();
            string name = rawName.Trim();

            if (!string.IsNullOrWhiteSpace(code))
            {
                var byCode = foodItems.FirstOrDefault(x =>
                    string.Equals(x.ItemCode, code, StringComparison.OrdinalIgnoreCase));
                if (byCode != null) return byCode;

                var byCodeAsName = foodItems.FirstOrDefault(x =>
                    string.Equals(x.Name, code, StringComparison.OrdinalIgnoreCase));
                if (byCodeAsName != null) return byCodeAsName;

                string codeKey = TextSearchHelper.ToSearchKey(code);
                var byCodeNormalized = foodItems.FirstOrDefault(x =>
                    string.Equals(TextSearchHelper.ToSearchKey(x.Name), codeKey, StringComparison.OrdinalIgnoreCase));
                if (byCodeNormalized != null) return byCodeNormalized;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                var byName = foodItems.FirstOrDefault(x =>
                    string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
                if (byName != null) return byName;

                string nameKey = TextSearchHelper.ToSearchKey(name);
                return foodItems.FirstOrDefault(x =>
                    string.Equals(TextSearchHelper.ToSearchKey(x.Name), nameKey, StringComparison.OrdinalIgnoreCase));
            }

            return null;
        }

        private static string GetCellText(IXLCell cell)
        {
            if (cell.IsEmpty()) return string.Empty;

            try
            {
                if (cell.DataType == XLDataType.DateTime)
                    return cell.GetDateTime().ToString("dd/MM/yyyy");

                return cell.GetString()?.Trim() ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static bool TryReadInt(IXLCell cell, out int value)
        {
            value = 0;
            if (cell.IsEmpty()) return false;

            if (cell.DataType == XLDataType.Number)
            {
                double number = cell.GetDouble();
                if (Math.Abs(number - Math.Round(number)) > 0.000001) return false;
                if (number < int.MinValue || number > int.MaxValue) return false;
                value = (int)Math.Round(number);
                return true;
            }

            string text = cell.GetString()?.Trim() ?? string.Empty;
            if (!TryParseFlexibleDecimal(text, out decimal decimalValue)) return false;
            if (decimalValue != Math.Truncate(decimalValue)) return false;
            if (decimalValue < int.MinValue || decimalValue > int.MaxValue) return false;

            value = (int)decimalValue;
            return true;
        }

        private static bool TryReadDecimal(IXLCell cell, out decimal value)
        {
            value = 0;
            if (cell.IsEmpty()) return false;

            if (cell.DataType == XLDataType.Number)
            {
                value = (decimal)cell.GetDouble();
                return true;
            }

            return TryParseFlexibleDecimal(cell.GetString(), out value);
        }

        private static bool TryReadDate(IXLCell cell, out DateTime value)
        {
            value = default;
            if (cell.IsEmpty()) return false;

            if (cell.DataType == XLDataType.DateTime)
            {
                value = cell.GetDateTime();
                return true;
            }

            string text = cell.GetString()?.Trim() ?? string.Empty;
            return DatabaseService.TryParseFlexibleDateTime(text, out value);
        }

        private static bool TryParseFlexibleDecimal(string? raw, out decimal value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(raw)) return false;

            string text = raw.Trim()
                .Replace("đ", "", StringComparison.OrdinalIgnoreCase)
                .Replace("₫", "", StringComparison.OrdinalIgnoreCase)
                .Replace(" ", "");

            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN"), out value))
                return true;

            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                return true;

            return false;
        }

        #endregion
    }
}
