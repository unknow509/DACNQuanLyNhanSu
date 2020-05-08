using System;
using System.Collections.Generic;

namespace QLNS_api_vue_test.Models
{
    public partial class Nhanvien
    {
        public Nhanvien()
        {
            Chitietchamcong = new HashSet<Chitietchamcong>();
            Hopdong = new HashSet<Hopdong>();
        }

        public int MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public string SoCmnd { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string NoiSinh { get; set; }
        public string HoKhau { get; set; }
        public string DienThoai { get; set; }
        public int? MaPhongBan { get; set; }
        public string MaChucVu { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }

        public virtual Chucvu MaChucVuNavigation { get; set; }
        public virtual Phongban MaPhongBanNavigation { get; set; }
        public virtual ICollection<Chitietchamcong> Chitietchamcong { get; set; }
        public virtual ICollection<Hopdong> Hopdong { get; set; }
    }
}
