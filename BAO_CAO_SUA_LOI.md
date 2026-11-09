# BÁO CÁO SỬA LỖI — QUẢN LÝ DỊCH VỤ KHÁCH SẠN

**Ngày:** 09/11/2026  
**Dự án:** `QuanLyDichVuKhachSan` (WPF, .NET 10, SQL Server)  
**Kết quả build:** thành công — **0 warning, 0 error**  
**Đã kiểm thử:** đăng nhập, sơ đồ phòng, check-out, in hóa đơn, lưu hóa đơn, khởi động lại, lịch sử giao dịch, báo cáo doanh thu.

---

## 1. NHỮNG GÌ ĐÃ SỬA

### 1.1. Cấu hình kết nối & khởi động

| Lỗi | Cách sửa |
|---|---|
| Không có `appsettings.json` trong source; fallback hard-code `Server=.\SQLEXPRESS01` (chỉ đúng máy tác giả) | Đổi fallback sang `Server=.`; thêm file cấu hình mẫu cho bản publish. Máy dùng instance khác chỉ cần sửa `appsettings.json`. |
| Khi mất kết nối SQL, các hàm `Load...FromDb` nuốt lỗi và trả list rỗng; app vẫn báo “khởi tạo thành công”, tự thêm tài khoản `admin/123` trong RAM | `DataService.InitializeAndSeedCache` bắt buộc `TestConnection()` trước khi nạp; lỗi được ném lên và hiển thị cho người dùng. Bỏ hoàn toàn fallback tài khoản admin/letan trong code. |
| Tự động “quyết toán” mọi đơn quá khứ mỗi lần mở app (`AutoSettlePastTransactions`), tự đánh dấu đã thanh toán, tự gán đối tác giặt ủi, tự tạo phân công ca | Mặc định **không chạy**. Chỉ chạy khi đặt biến môi trường `QLKS_AUTO_SETTLE_DEMO=1` cho dữ liệu demo. |
| Nút “Làm Mới”/reload giữ dữ liệu cũ khi DB rỗng | Đã sửa các khối nạp cache liên quan đến phòng/dịch vụ/hóa đơn để `Clear()` trước khi nạp. |

### 1.2. Giá phòng & hóa đơn quyết toán (lỗi nghiêm trọng nhất)

| Lỗi | Cách sửa |
|---|---|
| `PhongKhachSan` không có giá phòng; mọi phòng dùng mặc định 500.000đ, kể cả phòng đôi | Thêm cột `GiaPhong` vào schema; migration tự thêm cho DB cũ; seed: **Phòng đơn 500.000đ**, **Phòng đôi 800.000đ**. `LoadRoomsFromDb` đọc giá thật từ DB. |
| Check-out chỉ thu tiền dịch vụ; khách không có dịch vụ thì **không thu tiền phòng** | `CheckOutCommand` **luôn** mở hóa đơn quyết toán (tiền phòng + dịch vụ) trước khi trả phòng. |
| Hóa đơn hiển thị và tính tổng chỉ gồm dịch vụ (`RoomCost = 0`, `TotalAmount = ServicesCost`) | `PayAndExportBill` và `PayRoomServices` tính `RoomCost = Số đêm × Giá phòng`, `TotalAmount = RoomCost + ServicesCost`. Cửa sổ hóa đơn thêm dòng “Tiền phòng” và hiển thị đúng tổng. |
| Sau khi thanh toán dịch vụ, phòng vẫn “Đang ở”, phải bấm trả phòng lần 2 | Luồng check-out được gộp: thanh toán xong → tự chuyển phòng sang “Đang dọn”, xóa thông tin khách, kết thúc lượt lưu trú. |

**Kết quả kiểm thử thực tế:** phòng 101 (2 đêm × 500.000đ) + dịch vụ 515.000đ → hóa đơn **1.515.000đ**, lưu DB thành công.

### 1.3. Lưu trữ hóa đơn & doanh thu

| Lỗi | Cách sửa |
|---|---|
| `RoomInvoice` chỉ nằm trong RAM; tắt app là mất toàn bộ hóa đơn/doanh thu tiền phòng. Lại còn seed 2 hóa đơn mẫu giả mỗi lần khởi động | Thêm bảng `HoaDon` + `ChiTietHoaDon`; lưu hóa đơn + chi tiết trong transaction khi quyết toán; nạp lại khi khởi động. Đã bỏ 2 hóa đơn mẫu. |
| Báo cáo doanh thu không có tiền phòng | `GetServiceRevenueSummaries` thêm nhóm **“Tiền phòng”**; biểu đồ tròn và tổng doanh thu đã bao gồm tiền phòng. |
| Lịch sử giao dịch cộng trùng: hóa đơn (đã gồm dịch vụ) + từng đơn dịch vụ | Giao dịch hóa đơn phòng chỉ ghi nhận **phần tiền phòng**; các dịch vụ vẫn hiển thị riêng. Không còn cộng trùng. |
| `TotalAmountSum` cộng cả đơn chưa thanh toán/đã hủy | Thêm cờ `IsPaid` cho `TransactionRecord`; tổng tiền chỉ cộng giao dịch đã thanh toán, loại đơn hủy. |

### 1.4. Công nợ phòng & thanh toán dịch vụ

| Lỗi | Cách sửa |
|---|---|
| View công nợ phòng tính cả đơn ăn uống **đã hủy** | Thêm `TrangThai <> N'Đã hủy'` vào view và stored procedure. |
| View/SP tính cả đơn sự kiện/thuê xe **chưa tới giờ sử dụng** | Chỉ tính đơn có `ThoiGianBatDau/NgayThue <= thời điểm quyết toán`. |
| Thanh toán dịch vụ tự đánh dấu đơn ăn uống “Đã giao”, đơn xe “Hoàn tất” và gán `NgayTraThucTe` dù khách chưa trả xe | Thanh toán chỉ cập nhật công nợ; **không** thay đổi vòng đời dịch vụ. Việc giao món/trả xe vẫn do nghiệp vụ riêng xử lý. |
| Đơn ghi nợ phòng bị trừ “cọc ảo”: chọn ChargeToRoom nhưng `DepositAmount` vẫn bằng tổng tiền → hóa đơn phòng trừ cọc, thu thiếu 30–100% | Khi `ChargeToRoom`, `actualCollect = 0`; công nợ phòng là toàn bộ giá trị dịch vụ. |
| `sp_XacNhanThanhToanDichVuTheoPhong` gọi fire-and-forget, lỗi bị bỏ qua | Dùng bản đồng bộ `ConfirmRoomPayment`; nếu SQL từ chối, UI báo lỗi và **không** đánh dấu đã thanh toán trong RAM. |

### 1.5. Phòng — check-in/check-out

| Lỗi | Cách sửa |
|---|---|
| Cho phép check-in vào phòng **đang dọn** | Chặn ở cả `HotelRoom.CanCheckIn` và `CheckInCommand`; yêu cầu hoàn tất dọn phòng trước. |
| Cho phép chuyển phòng **đang có khách** sang “Đang dọn” (làm mất trạng thái lưu trú) | `CanSetCleaning` chỉ cho phòng trống/đang dọn; command chặn nếu phòng đang có khách, yêu cầu trả phòng trước. |
| Sau check-out, `KhachHang.SoPhong` vẫn giữ số phòng cũ → khách cũ hiện “đang lưu trú” | Khi trả phòng, cập nhật `KhachHang.SoPhong = NULL` theo phòng. |
| ComboBox phòng đang ở không cập nhật sau check-in/out | Gọi `RefreshAvailableRoomNumbers()` trong `NotifyAllStateChanges`. |
| Hóa đơn dùng ngày trả **dự kiến**, không có phụ thu trả muộn/trả sớm | **Chưa sửa triệt để** — xem mục 2.5. |

### 1.6. Sự kiện & thuê xe

| Lỗi | Cách sửa |
|---|---|
| Thêm/sửa khu vực sự kiện ghi trạng thái `Trống/Đang sử dụng/Bảo trì/Đang dọn` — vi phạm CHECK constraint (DB chỉ cho `Hoạt động`/`Ngừng khai thác`); lỗi bị nuốt, UI vẫn báo thành công | Map đúng: `Maintenance` → `Ngừng khai thác`, còn lại → `Hoạt động`; `await` và kiểm tra số dòng; bỏ ID giả khi insert thất bại. Khi load, `Ngừng khai thác` → `Maintenance` (không còn bị coi là sẵn sàng). |
| Sửa xe đang thuê ghi `Đang thuê` — vi phạm CHECK constraint; thêm xe ghi `Đang cho thuê` | Map đúng: `Available` → `Sẵn sàng`, `Maintenance` → `Bảo trì`, `Rented` → `Hoạt động`. `await` + kiểm tra affected rows; bỏ ID giả. |
| Sửa xe tự đặt lại cọc = 30% giá thuê, ghi đè cọc thực tế | Giữ nguyên `TienCocQuyDinh` khi sửa; dialog không có trường cọc nên không được tự đổi. |
| Xe ô tô bị lưu loại “Xe côn tay” | Map đủ 4 loại, thêm `Car → Xe ô tô`. |
| Parse giá bằng `Replace(",", "").Replace(".", "")` làm “1,5” thành “15” | Dùng `decimal.TryParse` theo culture `vi-VN` (app đã set culture). |

### 1.7. Món ăn & tồn kho

| Lỗi | Cách sửa |
|---|---|
| Bán món không kiểm tra tồn kho; gọi `sp_DatMonAn` nhưng bỏ qua kết quả; hết hàng vẫn tạo đơn, UI báo thành công | Kiểm tra tồn kho trước; nếu `DatMonAn` trả thất bại thì ném lỗi, không tạo đơn; chỉ trừ kho RAM sau khi SP thành công. |
| Insert header + chi tiết đơn hàng fire-and-forget | Chạy đồng bộ, kiểm tra số dòng ảnh hưởng; lỗi được báo thay vì im lặng. |

### 1.8. Bãi giữ xe

| Lỗi | Cách sửa |
|---|---|
| `ActualOutTime` lấy lúc mở app, không phải lúc xe ra → tính phí thiếu | Khi chọn vé, đặt giờ ra = hiện tại; khi quyết toán, nếu giờ ra < giờ vào thì reset về hiện tại và tính lại phí. |

### 1.9. Nhân sự

| Lỗi | Cách sửa |
|---|---|
| Cho nhân viên nghỉ việc nhưng tài khoản đăng nhập vẫn hoạt động | Khi nghỉ việc, cập nhật thêm `TaiKhoan.TrangThai = 0` theo họ tên; đồng bộ đồng bộ (không fire-and-forget). |

### 1.10. Cơ sở dữ liệu

- Thêm bảng `HoaDon`, `ChiTietHoaDon`; cột `PhongKhachSan.GiaPhong`.
- Sửa `vw_DichVuChuaThanhToanTheoPhong`, `vw_DoanhThuTheoDichVu`, `sp_XacNhanThanhToanDichVuTheoPhong`.
- Thêm **script migration không mất dữ liệu**: `Database/Migration_2026-11-09_HotelFixes.sql`.
- App tự bổ sung cột/bảng còn thiếu khi khởi động (đối với DB cũ).

---

## 2. NHỮNG GÌ CHƯA ĐÚNG LOGIC / CHƯA SỬA — VÀ CÁCH SỬA ĐÚNG

> Đây là các vấn đề còn tồn tại. Với một app quản lý khách sạn lớn, các mục **Critical** nên làm trước.

### 2.1. Critical — cần sửa trước khi dùng thật

1. **Chưa có nghiệp vụ đặt phòng (reservation) và mã lượt lưu trú (StayId).**
   - Hiện tại khách chỉ được gán trực tiếp vào phòng; không đặt trước, không kiểm tra phòng trống theo ngày, không chống trùng đặt phòng.
   - **Cách sửa đúng:** tạo bảng `DatPhong` (KhachHangId, PhongId, NgayNhan, NgayTra, TrangThai, TienCoc) và `LuotLuuTru` (StayId). Mọi dịch vụ (`DonHangMonAn`, `DonDatSuKien`, `DonThueXe`, `BaiDoXe`, `DonGiatUi`) phải có `LuotLuuTruId` thay vì chỉ `SoPhong`.
   - Thêm view/trigger chống trùng khoảng thời gian; trạng thái phòng (`Trống/Đang ở/Đang dọn/Bảo trì`) phải tính động theo ngày giờ.

2. **Công nợ đang gom theo `SoPhong`, không theo lượt lưu trú.**
   - Khách cũ chưa thanh toán có thể bị tính cho khách mới khi cùng phòng; dịch vụ phát sinh ngoài khoảng lưu trú vẫn bị gom.
   - **Cách sửa đúng:** dùng `StayId`; view công nợ lọc theo `StayId`; khi check-out chỉ quyết toán dịch vụ thuộc lượt đó.

3. **Hóa đơn và thanh toán chưa nằm trong cùng một transaction.**
   - Hiện `ConfirmRoomPayment` (SP) và `SaveInvoice` là hai lần gọi riêng. Nếu lưu hóa đơn lỗi sau khi SP đã thu tiền, lịch sử vẫn thiếu.
   - **Cách sửa đúng:** gộp vào một stored procedure `sp_QuyetToanPhong` (mark services paid + insert `HoaDon`/`ChiTietHoaDon` + cập nhật phòng) trong một transaction, trả về `InvoiceId`. Gọi đúng một lần từ app.

4. **Bảo mật mật khẩu chưa đạt chuẩn.**
   - `SecurityHelper.HashPassword` thực chất là **mã hóa AES hai chiều**, khóa/IV hard-code trong binary; admin xem được mật khẩu plaintext; `DecodePasswordFromDb` còn dò các mật khẩu phổ biến; tài khoản mẫu `admin/123`, `letan/123`.
   - **Cách sửa đúng:** dùng PBKDF2/BCrypt/Argon2 với salt riêng từng user; bỏ hoàn toàn giải mã; không hiển thị mật khẩu (thay bằng “Đặt lại mật khẩu”); bắt buộc đổi mật khẩu lần đầu; xóa tài khoản demo khỏi script production.

5. **Ghi CSDL kiểu fire-and-forget còn rải rác.**
   - Nhiều ViewModel/View dùng `_ = ExecuteNonQueryAsync(...)`: lỗi trigger/CHECK/FK bị nuốt, RAM và DB lệch nhau, UI vẫn báo thành công.
   - **Cách sửa đúng:** chuyển các command sang `async Task`, `await` và kiểm tra kết quả; dùng transaction; nếu DB lỗi thì rollback trạng thái RAM và hiển thị lỗi gốc. Không dùng ID giả khi insert thất bại.

6. **Script `Database/QuanLyDichVuKhachSan.sql` xóa toàn bộ database khi chạy.**
   - **Cách sửa đúng:** tách `CreateDatabase.sql` chỉ dùng cho dev; production dùng script migration có backup; không `DROP DATABASE` trong script cập nhật.

### 2.2. High — nghiệp vụ khách sạn chưa chuẩn

7. **Chưa có phụ thu trả muộn/trả sớm, thuế, giảm giá, cọc phòng.**
   - `StayNights` dùng ngày trả dự kiến; khách trả muộn không bị phụ thu, trả sớm vẫn tính đủ đêm.
   - **Cách sửa đúng:** lưu `NgayTraThucTe`; chính sách late checkout theo giờ/ngày; thêm `Thue`, `GiamGia`, `TienCoc` vào hóa đơn; hiển thị breakdown rõ ràng.

8. **Sự kiện/thuê xe/giặt ủi chưa chặn ngày quá khứ, chưa kiểm tra trạng thái tài nguyên.**
   - Có thể tạo đơn sự kiện trong quá khứ, đặt sảnh “Ngừng khai thác”, thuê xe đang bảo trì.
   - **Cách sửa đúng:** validate `start >= now`; kiểm tra trạng thái khai thác; kiểm tra trùng lịch trước khi lưu; hiển thị lỗi từ trigger.

9. **Mô hình trạng thái sảnh/xe còn lẫn giữa “trạng thái khai thác” và “trạng thái hiện tại”.**
   - DB `KhuVucSuKien.TrangThai`/`XeChoThue.TrangThai` chỉ nên là `Hoạt động/Ngừng khai thác` (và `Bảo trì` cho xe); `Trống/Đang sử dụng/Đang dọn/Đang thuê` phải tính động từ lịch.
   - **Cách sửa đúng:** giữ nguyên cột khai thác; dùng view trạng thái hiện tại; UI không cho nhập trực tiếp trạng thái động.

10. **Kho: chưa quản lý hạn sử dụng/FIFO, chưa chặn số lượng âm.**
    - `sp_DatMonAn` chỉ trừ tổng tồn; lô hết hạn vẫn bán; `@SoLuong` âm làm tăng kho.
    - **Cách sửa đúng:** `IF @SoLuong <= 0 RETURN 0`; trừ FIFO theo `HanSuDung`; chặn bán lô hết hạn; báo cáo giá vốn theo lô.

11. **Nhân viên ↔ tài khoản chưa có khóa ngoại.**
    - Khóa tài khoản hiện dò theo `HoTen`, có thể trùng tên hoặc không khớp.
    - **Cách sửa đúng:** thêm `TaiKhoan.NhanVienId` FK; khi nghỉ việc khóa theo ID; lưu `NhanVienId` + `TaiKhoanId` trên mọi giao dịch để audit.

12. **Bãi xe: chưa chống trùng biển số đang gửi, miễn phí cho mọi số phòng gõ vào, chưa có vé tháng.**
    - **Cách sửa đúng:** unique filtered index cho vé mở theo biển số; kiểm tra phòng đang có khách trước khi `ResidentFree`; thêm `MonthlyPass` + hạn vé.

13. **Báo cáo doanh thu còn trộn gộp/hoa hồng và lọc theo ngày phát sinh.**
    - Giặt ủi cộng hoa hồng, dịch vụ khác cộng 100%; báo cáo theo ngày sử dụng thay vì ngày thu tiền.
    - **Cách sửa đúng:** thống nhất “doanh thu gộp” vs “lợi nhuận ròng”; thêm cột `ThoiGianThanhToan` cho báo cáo dòng tiền; tách báo cáo theo ngày phát sinh và ngày thu.

14. **Reload dữ liệu còn giữ cache cũ khi DB rỗng; timer cảnh báo không dừng khi logout.**
    - **Cách sửa đúng:** luôn `Clear()` kể cả list rỗng; `StopMonitoring`/dispose timer khi logout/đóng app.

### 2.3. Medium — nên sửa để hoàn thiện

15. Đơn hàng/mã đơn dùng `Max(Id)+1` và `Count+1` → dễ trùng khi nhiều máy hoặc xóa dữ liệu. Dùng `IDENTITY`/sequence.
16. `AddInventoryBatch`, `AddNewVehicleCommand`, các hàm quota trong `DatabaseService` là dead code và ghi cột không tồn tại — nên xóa.
17. `GetAllTransactions` hard-code tên nhân viên khi thiếu dữ liệu — nên hiển thị “Không xác định” hoặc lấy từ DB.
18. Lọc giao dịch theo loại đang case-sensitive — dùng `IndexOf(..., OrdinalIgnoreCase)`.
19. Thống kê lương nhân viên luôn hiển thị lương tháng đầy đủ dù chọn “Hôm nay” — cần prorate hoặc ghi rõ.
20. `TotalStockValue` dùng giá bán thay vì giá vốn — đổi sang `AverageCostPrice`.
21. `TrashedDate` không lưu DB nên tính năng tự xóa sau 3 tháng không bao giờ chạy — thêm cột `NgayXoa`.
22. Thêm/sửa/xóa mặt hàng, đối tác giặt ủi cập nhật RAM trước, DB sau — cần await + rollback.
23. `CustomerViewModel` gộp khách theo họ tên và coi khách còn `SoPhong` là đang lưu trú — dùng ID/CCCD/SĐT và join trạng thái phòng.
24. `NotificationService` giữ key thông báo vô hạn — dọn theo ngày.
25. Global exception handler `e.Handled = true` cho mọi lỗi UI, chỉ ghi Debug — cần log file/EventLog, hiển thị mã lỗi và cho phép restart.

### 2.4. Low — chất lượng code

- Nhiều chuỗi tiếng Việt bị lưu dạng không dấu/mojibake ở một số file cũ; nên chuẩn hóa UTF-8.
- Thiếu single-instance lock; dễ mở 2 app và trùng khóa.
- Một số `async void` trong dialog chưa bọc `try/catch` sau `await`.
- `InputHelper.DecimalOnly` cho phép dấu phẩy nhưng nơi parse lại xóa dấu phẩy — thống nhất một hàm parse tiền tệ.
- Nên thêm unit test cho tính tiền phòng, công nợ, check-out.

---

## 3. HƯỚNG DẪN CHẠY & CẬP NHẬT CSDL

### Yêu cầu
- Windows 10/11, **.NET 10 SDK/Runtime** (project target `net10.0-windows`).
- SQL Server (Express/Developer/LocalDB) chạy cùng máy.

### Cài đặt mới (mất dữ liệu — chỉ dùng cho dev/demo)
```powershell
sqlcmd -S . -E -i Database\QuanLyDichVuKhachSan.sql -f 65001 -b
```

### Cập nhật CSDL đang có (không mất dữ liệu)
```powershell
sqlcmd -S . -E -d QuanLyDichVuKhachSan -i Database\Migration_2026-11-09_HotelFixes.sql -f 65001 -b
```

### Cấu hình kết nối
Tạo `appsettings.json` cạnh file chạy:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=QuanLyDichVuKhachSan;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
Nếu dùng SQL Express đặt `Server=.\SQLEXPRESS`; LocalDB đặt `Server=(LocalDB)\MSSQLLocalDB`.
File này bị `.gitignore` (chứa cấu hình máy) — không commit.

### Build & chạy
```powershell
dotnet build QuanLyDichVuKhachSan.slnx
dotnet run
```
Bản publish nằm trong `publish/` (đã cập nhật exe/DLL mới).

### Tài khoản demo
- `admin` / `123` (Admin)
- `letan` / `123` (Lễ tân)
> Đây là tài khoản demo yếu, cần đổi/khóa trước khi dùng thật (xem mục 2.1.4).

---

## 4. DANH SÁCH FILE ĐÃ SỬA

| File | Nội dung |
|---|---|
| `Database/QuanLyDichVuKhachSan.sql` | Giá phòng, bảng hóa đơn, view công nợ/doanh thu, SP thanh toán |
| `Database/Migration_2026-11-09_HotelFixes.sql` | Migration không mất dữ liệu cho DB cũ |
| `Models/RoomModels.cs` | `RoomInvoice.Id`, chặn check-in phòng đang dọn |
| `Models/ServiceModels.cs` | `TransactionRecord.IsPaid` |
| `Services/DataService.cs` | Khởi tạo DB, giá phòng, hóa đơn, công nợ, thống kê, tồn kho, bỏ hóa đơn mẫu |
| `Services/DatabaseService.cs` | Migration giá phòng/hóa đơn, lưu/nạp hóa đơn, đọc giá phòng, xác nhận thanh toán đồng bộ |
| `ViewModels/RoomMapViewModel.cs` | Luồng check-out/quyết toán, chặn phòng dọn, refresh phòng, xóa khách khỏi phòng |
| `ViewModels/EventViewModel.cs` / `VehicleViewModel.cs` | Không trừ cọc ảo khi ghi nợ phòng, `InitialPaymentType` |
| `ViewModels/ParkingViewModel.cs` | Giờ xe ra theo thời điểm quyết toán |
| `ViewModels/StaffViewModel.cs` | Khóa tài khoản khi nhân viên nghỉ việc |
| `ViewModels/TransactionHistoryViewModel.cs` | Tổng tiền chỉ tính giao dịch đã thanh toán |
| `Views/AddEventSpaceDialogWindow.xaml.cs` | Trạng thái hợp lệ, bỏ ID giả, parse giá đúng |
| `Views/EditEventSpaceDialogWindow.xaml.cs` | Trạng thái hợp lệ, await + báo lỗi |
| `Views/AddVehicleDialogWindow.xaml.cs` | Trạng thái/loại xe hợp lệ, bỏ ID giả, parse giá đúng |
| `Views/EditVehicleDialogWindow.xaml.cs` | Trạng thái hợp lệ, không ghi đè cọc, await + báo lỗi |
| `Views/InvoiceBillWindow.xaml.cs` | Hiển thị tiền phòng + tổng thanh toán đúng |

---

## 5. KẾT QUẢ KIỂM THỬ

- Build Debug/Release: **0 warning, 0 error**.
- Đăng nhập `admin/123`: OK.
- Sơ đồ phòng: hiển thị đúng trạng thái, khách, ngày nhận/trả, dịch vụ.
- Check-out phòng 101:
  - 2 đêm × 500.000đ = 1.000.000đ.
  - Dịch vụ: 55.000đ + 300.000đ + 160.000đ = 515.000đ.
  - Tổng hóa đơn: **1.515.000đ** (đúng).
  - Lưu `HoaDon` + 3 dòng `ChiTietHoaDon`.
  - Phòng chuyển “Đang dọn”, thông tin khách được xóa.
- Khởi động lại app:
  - Hóa đơn vẫn còn trong lịch sử giao dịch.
  - Báo cáo doanh thu có nhóm “Tiền phòng”.
  - Không còn 2 hóa đơn mẫu giả.
- Bản publish chạy được, `publish/appsettings.json` trỏ `Server=.`.

---

*Báo cáo được tạo trong quá trình sửa lỗi ngày 09/11/2026. Các mục ở phần 2 là đề xuất kỹ thuật, chưa được triển khai trong lần sửa này.*
