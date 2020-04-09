using System;
using System.Collections.Generic;

namespace QLNS_api_vue_test.Models
{
    public partial class Chamcong
    {
        public Chamcong()
        {
            Chitietchamcong = new HashSet<Chitietchamcong>();
        }

        public DateTime Day { get; set; }

        public virtual ICollection<Chitietchamcong> Chitietchamcong { get; set; }
    }
}
