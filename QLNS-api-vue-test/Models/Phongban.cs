using System;
using System.Collections.Generic;

namespace QLNS_api_vue_test.Models
{
    public partial class Phongban
    {
        public Phongban()
        {
            Nhanvien = new HashSet<Nhanvien>();
        }

        public int MaPhongBan { get; set; }
        public string TenPhongBan { get; set; }

        public virtual ICollection<Nhanvien> Nhanvien { get; set; }
    }
}
