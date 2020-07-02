using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLNS_api_vue_test.Models;
using Telegram.Bot;
using QLNS_api_vue_test.Controllers;

namespace QLNS_api_vue_test.Controllers
{
    [Route("api/[controller]")]
    public class AssigneController : Controller
    {
        private DACNQuanLyNhanSuContext db = new DACNQuanLyNhanSuContext();
        public static readonly TelegramBotClient bot = new TelegramBotClient("1251187799:AAFlWJPB64OAWH5aXg0FPaqJ_aZAfj3vlkI");
        //*************************************GET**************************************
        [HttpGet("findallassigne/{MAPHONGBAN}")]
        public async Task<IActionResult> findallassigne(int maphongban)
        {
            try
            {
                var nhanviens = from nv in db.Nhanvien
                                join pb in db.Phongban
                                on nv.MaPhongBan equals pb.MaPhongBan
                                join cv in db.Chucvu
                                on nv.MaChucVu equals cv.MaChucVu
                                where nv.MaPhongBan == maphongban
                                select new
                                {
                                    nv.MaNhanVien,
                                    nv.HoTen,
                                    nv.GioiTinh,
                                    nv.SoCmnd,
                                    nv.NgaySinh,
                                    nv.NoiSinh,
                                    nv.HoKhau,
                                    nv.DienThoai,
                                    nv.MaPhongBan,
                                    pb.TenPhongBan,
                                    nv.MaChucVu,
                                    cv.TenChucVu,
                                    nv.TenDangNhap,
                                    nv.MatKhau,
                                    thucLanh = cv.HeSoLuong * 20 * 8,
                                };
                return Ok(nhanviens);
            }
            catch(Exception e)
            {
                DateTime date = DateTime.Now;
                date.ToString("dddd, dd MMMM yyyy");
                var t = await bot.SendTextMessageAsync(-388649962, "/Assigne/findallassigne: " + date);
                return BadRequest(e);
            }
        }
        [HttpPost("assigne")]
        public async Task<IActionResult> Assigne([FromBody] Phancong phancong)
        {
            try
            {
                //nhanvien.MaNhanVien = null;
                db.Phancong.Add(phancong);
                db.SaveChanges();
                return Ok(phancong);
            }
            catch(Exception e)
            {
                DateTime date = DateTime.Now;
                date.ToString("dddd, dd MMMM yyyy");
                var t = await bot.SendTextMessageAsync(-388649962, "/Assigne/assigne: " + date);
                return BadRequest(e);
            }
        }
        [HttpGet("getdesassigne/{manhanvien}")]
        public async Task<ActionResult> getdesassigne(int manhanvien)
        {
            try
            {
                Phancong phancong = db.Phancong.SingleOrDefault(n => n.MaNhanVien == manhanvien);
                return Ok(phancong);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPut("modifystatus")]
        public async Task<IActionResult> modifyStatus([FromBody] Phancong phancong)
        {
            try
            {
                db.Entry(phancong).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return Ok(phancong);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


    }
}