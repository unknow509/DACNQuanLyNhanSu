using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;

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
        
        public static readonly TelegramBotClient bot = new TelegramBotClient("1251187799:AAFlWJPB64OAWH5aXg0FPaqJ_aZAfj3vlkI");

        private DACNQuanLyNhanSuContext db = new DACNQuanLyNhanSuContext();
         
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginNhanVien nhanvien)
        {

            var nv = db.Nhanvien.FirstOrDefault(t => t.TenDangNhap == nhanvien.username && t.MatKhau == nhanvien.password);
            if (nv != null)
            {
                DateTime date = DateTime.Now;
                date.ToString("dddd, dd MMMM yyyy");
                
                var t = await bot.SendTextMessageAsync(-388649962,"khách đã đăng nhập lúc: " + date);

                return Ok(nv);

            }
            else
            {
                DateTime date = DateTime.Now;
                date.ToString("dddd, dd MMMM yyyy");
                var t = await bot.SendTextMessageAsync(-388649962, "khách đã đăng nhập lúc: " + date);
                return NotFound();
            }
        }

    }
}
