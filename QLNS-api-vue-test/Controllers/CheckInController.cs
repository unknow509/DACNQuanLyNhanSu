using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLNS_api_vue_test.Models;

namespace QLNS_api_vue_test.Controllers
{
    [Route("api/[controller]")]
    public class CheckInController : Controller
    {
        private DACNQuanLyNhanSuContext db = new DACNQuanLyNhanSuContext();
        //*************************************GET**************************************
        [HttpGet("GetAllPending")]
        public async Task<IActionResult> GetAllPending()
        {
            try
            {
                //var pending = db.Chitietchamcong.Where(t=>t.Status=="Pending");
                //pending.ToList();
                var pending = from nv in db.Nhanvien
                              join ctcc in db.Chitietchamcong
                              on nv.MaNhanVien equals ctcc.MaNhanVien
                              where ctcc.Status == "Pending"
                              select new
                              {
                                  ctcc.Status,
                                  ctcc.MaNhanVien,
                                  ctcc.GioBatDau,
                                  ctcc.GioKetThuc,
                                  ctcc.Day,
                                  nv.HoTen
                                    };
                return Ok(pending);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        //*************************************POST************************************** 
        [HttpPost("CheckIn")]/* checkin giờ vào*/
        public async Task<IActionResult> CheckIn([FromBody] Chitietchamcong TimeCheckIn)
        {
            try
            {
                db.Chitietchamcong.Add(TimeCheckIn);
                db.SaveChanges();
                return Ok(TimeCheckIn);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        //*************************************PUT**************************************
        [HttpPut("Update")] /*  chi tiết chấm công */
        public async Task<IActionResult> Update([FromBody] Chitietchamcong ctcc)
        {
            try
            {
                db.Entry(ctcc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                db.SaveChanges();
                return Ok(ctcc);
            }
            catch
            {
                return BadRequest();
            }
        }
        //*************************************DELETE**************************************
    }
}