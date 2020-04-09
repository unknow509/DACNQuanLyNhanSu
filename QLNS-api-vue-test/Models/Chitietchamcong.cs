using System;
using System.Collections.Generic;

namespace QLNS_api_vue_test.Models
{
    public partial class Chitietchamcong
    {
        public int MaNhanVien { get; set; }
        public DateTime Day { get; set; }
        public DateTime? GioBatDau { get; set; }
        public DateTime? GioKetThuc { get; set; }
        public string Status { get; set; }

        public virtual Chamcong DayNavigation { get; set; }
        public virtual Nhanvien MaNhanVienNavigation { get; set; }
    }
}
