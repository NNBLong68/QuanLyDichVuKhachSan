using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyDichVuKhachSan.Helpers
{
    public static class StaffAccountHelper
    {
        /// <summary>
        /// Tự động sinh tên tài khoản nhân viên theo cấu trúc:
        /// Tên + 2 ký tự đầu của họ và tên lót (VD: "Lê Văn Lâm" -> "lamlv", "Trần Thị Mai" -> "maitt").
        /// Tự động đánh số lũy tiến nếu trùng lặp (lamlv1, lamlv2...).
        /// </summary>
        public static string GenerateStaffUsername(string fullName, IEnumerable<string>? existingUsernames = null)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return "nv" + DateTime.Now.ToString("fff");
            }

            // Xóa dấu tiếng Việt và chuẩn hóa
            string unaccented = TextSearchHelper.RemoveDiacritics(fullName.Trim());
            var words = unaccented.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0)
            {
                return "nv" + DateTime.Now.ToString("fff");
            }

            // 1. Tên chính là từ cuối cùng (ví dụ: "Lâm" -> "lam")
            string rawFirstName = words[^1];
            var sbFirstName = new StringBuilder();
            foreach (char c in rawFirstName)
            {
                if (char.IsLetter(c)) sbFirstName.Append(char.ToLowerInvariant(c));
            }
            string firstName = sbFirstName.Length > 0 ? sbFirstName.ToString() : "nv";

            // 2. Họ và tên lót là các từ phía trước
            // Ví dụ "Lê Văn Lâm": Họ "Lê" -> 'l', Lót "Văn" -> 'v' => 2 ký tự 'lv'
            var sbInitials = new StringBuilder();
            for (int i = 0; i < words.Length - 1; i++)
            {
                string w = words[i];
                for (int j = 0; j < w.Length; j++)
                {
                    if (char.IsLetter(w[j]))
                    {
                        sbInitials.Append(char.ToLowerInvariant(w[j]));
                        break;
                    }
                }
            }

            string initials = sbInitials.ToString();
            // Lấy 2 ký tự đầu theo đúng quy tắc (ví dụ Họ + Lót = 2 ký tự)
            if (initials.Length > 2)
            {
                initials = initials.Substring(0, 2);
            }

            string baseUsername = firstName + initials;
            if (string.IsNullOrWhiteSpace(baseUsername))
            {
                baseUsername = "nhanvien";
            }

            // 3. Kiểm tra trùng lặp với danh sách tài khoản hiện hữu
            var existingSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (existingUsernames != null)
            {
                foreach (var u in existingUsernames)
                {
                    if (!string.IsNullOrWhiteSpace(u))
                    {
                        existingSet.Add(u.Trim());
                    }
                }
            }

            if (!existingSet.Contains(baseUsername))
            {
                return baseUsername;
            }

            // Nếu trùng, thêm số thứ tự tăng dần: lamlv1, lamlv2, ...
            int counter = 1;
            while (existingSet.Contains($"{baseUsername}{counter}"))
            {
                counter++;
            }
            return $"{baseUsername}{counter}";
        }
    }
}

