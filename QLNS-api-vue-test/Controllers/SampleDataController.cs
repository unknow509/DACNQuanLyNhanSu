using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLNS_api_vue_test.Models;

namespace QLNS_api_vue_test.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private DACNQuanLyNhanSuContext db = new DACNQuanLyNhanSuContext();
        //*************************************GET**************************************
        [HttpGet("findall")]
        public async Task<IActionResult> findall()
        {
            try
            {
                var nhanviens = from nv in db.Nhanvien
                                join pb in db.Phongban
                                on nv.MaPhongBan equals pb.MaPhongBan
                                join cv in db.Chucvu
                                on nv.MaChucVu equals cv.MaChucVu
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
                                    nv.TenDangNhap,
                                    nv.MatKhau,
                                    thucLanh = cv.HeSoLuong * 20 * 8,
                                };
                return Ok(nhanviens);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("find/{MANHANVIEN}")]
        public async Task<IActionResult> find(int MaNhanVien)
        {
            try
            {
                var id = db.Nhanvien.Find(MaNhanVien);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("findPhongBan")]
        public async Task<IActionResult> findPhongBan()
        {
            try
            {
                var phongban = db.Phongban.ToList();
                return Ok(phongban);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("CallPosition")]
        public async Task<IActionResult> CallPosition()
        {
            try
            {
                var chucvu = db.Chucvu.ToList();
                return Ok(chucvu);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        //*************************************POST**************************************

        [HttpPost("create")] /* tạo nhân viên*/
        public async Task<IActionResult> Create([FromBody] Nhanvien nhanvien)
        {
            try
            {
                //nhanvien.MaNhanVien = db.Nhanvien.Max(t => t.MaNhanVien) + 1;
                db.Nhanvien.Add(nhanvien);
                db.SaveChanges();
                var singlenv = db.Nhanvien.OrderByDescending(x => x.MaNhanVien).First();
                var GetSingle = from nv in db.Nhanvien
                                join pb in db.Phongban
                                on nv.MaPhongBan equals pb.MaPhongBan
                                where singlenv.MaNhanVien == nv.MaNhanVien
                                select new
                                {
                                    nv.HoTen,
                                    nv.GioiTinh,
                                    nv.NgaySinh,
                                    nv.HoKhau,
                                    nv.DienThoai,
                                    pb.TenPhongBan,
                                    nv.TenDangNhap,
                                    nv.MatKhau,
                                };

                return Ok(GetSingle);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("CreateDeparment")] /* tạo phòng ban */
        public async Task<IActionResult> CreateDeparment([FromBody] Phongban phongban)
        {
            try
            {
                //nhanvien.MaNhanVien = db.Nhanvien.Max(t => t.MaNhanVien) + 1;
                db.Phongban.Add(phongban);
                db.SaveChanges();
                return Ok(phongban);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        //*************************************PUT**************************************

        [HttpPut("update")] /*  nhân viên */
        public async Task<IActionResult> Update([FromBody] Nhanvien nhanvien)
        {
            try
            {
                //nhanvien.MaNhanVien = null;
                db.Entry(nhanvien).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                db.SaveChanges();
                return Ok(nhanvien);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateDeparment")] /*  phòng ban */
        public async Task<IActionResult> UpdateDeparment([FromBody] Phongban phongban)
        {
            try
            {
                //nhanvien.MaNhanVien = null;
                db.Entry(phongban).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                db.SaveChanges();
                return Ok(phongban);
            }
            catch
            {
                return BadRequest();
            }
        }
        //*************************************DELETE**************************************
        [HttpDelete("delete/{manhanvien}")]
        public async Task<IActionResult> Delete(int MaNhanVien)
        {
            try
            {
                var id = db.Nhanvien.Find(MaNhanVien);
                db.Nhanvien.Remove(id);

                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteDeparment/{maphongban}")]
        public async Task<IActionResult> DeleteDeparment(int MaPhongBan)
        {
            try
            {
                var id = db.Phongban.Find(MaPhongBan);
                db.Phongban.Remove(id);

                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
