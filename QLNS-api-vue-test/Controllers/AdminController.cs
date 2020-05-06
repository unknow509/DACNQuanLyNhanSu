using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using QLNS_api_vue_test.Models;

namespace QLNS_api_vue_test.Controllers
{
    public class LoginNhanVien
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    [Route("api/[controller]")]
    public class AdminController : Controller
    {

        private DACNQuanLyNhanSuContext db = new DACNQuanLyNhanSuContext();

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginNhanVien nhanvien)
        {

            var nv = db.Nhanvien.FirstOrDefault(t => t.TenDangNhap == nhanvien.username && t.MatKhau == nhanvien.password);
            if(nv != null){
                return Ok(nv);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
