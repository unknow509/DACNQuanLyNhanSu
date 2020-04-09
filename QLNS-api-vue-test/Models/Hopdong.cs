using System;
using System.Collections.Generic;

namespace QLNS_api_vue_test.Models
{
    public partial class Hopdong
    {
        public int MaLoaiHopDong { get; set; }
        public string TenHopDong { get; set; }
        public DateTime? NgayVaoLam { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public int? MaNhanVien { get; set; }

        public virtual Nhanvien MaNhanVienNavigation { get; set; }
    }
}
