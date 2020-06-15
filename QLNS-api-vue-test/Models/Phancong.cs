using System;
using System.Collections.Generic;

namespace QLNS_api_vue_test.Models
{
    public partial class Phancong
    {
        public int MaPhanCong { get; set; }
        public string NoiDungCongViec { get; set; }
        public int? MaNhanVien { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public string TienDo { get; set; }
    }
}
