using System;
using System.Collections.Generic;

namespace QLNS_api_vue_test.Models
{
    public partial class Chucvu
    {
        public Chucvu()
        {
            Nhanvien = new HashSet<Nhanvien>();
        }

        public string MaChucVu { get; set; }
        public string TenChucVu { get; set; }
        public double? HeSoLuong { get; set; }

        public virtual ICollection<Nhanvien> Nhanvien { get; set; }
    }
}
