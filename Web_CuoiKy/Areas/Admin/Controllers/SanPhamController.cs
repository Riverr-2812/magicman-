using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web_CuoiKy.Data;
using Web_CuoiKy.Models;

namespace Web_CuoiKy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SanPhamController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {

            IEnumerable<Sanpham> sp = _db.sanphams.Include("nhacungcaps").ToList();
   


            return View(sp);
        }
        [HttpGet]
        public IActionResult Upsert(int id)
        {
            Sanpham sp = new Sanpham();
            IEnumerable<SelectListItem> dstheloai = _db.nhacungcaps.Select(

                item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                }

                );
            ViewBag.DSTHELOAI = dstheloai;
            if (id == 0)
            {
                return View(sp);
            }
            else
            {
                sp = _db.sanphams.Include("nhacungcaps").FirstOrDefault(sp => sp.Id == id);
                return View(sp);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Sanpham sp)
        {
            if (ModelState.IsValid)
            {
                if (sp.Id == 0)
                {
                    _db.sanphams.Add(sp);
                }
                else
                {
                    _db.sanphams.Update(sp);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {


                var sp = _db.sanphams.Find(id);
                return View(sp);
            }
        }
        [HttpPost]
        public IActionResult Deletes(int id)
        {
            var SP = _db.sanphams.Find(id);
            if (SP == null)
            {
                return Json(new { success = false });
            }

            try
            {
                _db.sanphams.Remove(SP);
                _db.SaveChanges();
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [HttpGet]
        public IActionResult Details(int SanphamId)
        {
            GioHang giohang = new GioHang()
            {
                sanphamId = SanphamId,
                sanphams = _db.sanphams.Include(sp => sp.nhacungcaps).FirstOrDefault(sp => sp.Id == SanphamId),
             

            };
        
                return View(giohang);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(GioHang giohang)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
			var claims = identity.FindFirst(ClaimTypes.Name);

            giohang.Username = claims.Value;
            giohang.AppUserId = claim.Value;
     
            var giohangkt = _db.gioHangs.FirstOrDefault(sp => sp.sanphamId == giohang.sanphamId && sp.AppUserId == giohang.AppUserId );
            
            if(giohangkt == null)
            {

                _db.gioHangs.Add(giohang);
            }
            else
            {
                giohangkt.Quanlity += giohang.Quanlity;
            }
       
            _db.SaveChanges();
        
            return RedirectToAction("GioHang");
        }
        [Authorize]
        public IActionResult GioHang()
        {

            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            GioHangViewModel giohang = new GioHangViewModel
            {
                dsgioHang = _db.gioHangs
              .Include(gh => gh.sanphams)
              .Where(gh => gh.AppUserId == claim.Value)
              .ToList(),
              hoaDons = new HoaDon()
            };
            foreach(var item in giohang.dsgioHang)
            {
                item.giatien = item.Quanlity * Convert.ToInt32(item.sanphams.Gia);
                giohang.hoaDons.Total += item.giatien;
            }
			return View(giohang);
        
    }
      
        public IActionResult Xoasp(int xoasp)
        {
            var sp = _db.gioHangs.FirstOrDefault(item => item.sanphamId == xoasp);
            if(sp == null)
            {
                return RedirectToAction("GioHang");
            }
            else
            {
             
                    _db.gioHangs.Remove(sp);
                
        
            }
      
            _db.SaveChanges();
            return RedirectToAction("GioHang");
          
        }
        public IActionResult Trusp(int layid) 
        { 
            var xoa = _db.gioHangs.FirstOrDefault(sp => sp.Id == layid);
            xoa.Quanlity -= 1;
            if(xoa.Quanlity == 0)
            {
                _db.gioHangs.Remove(xoa);
            }
            _db.SaveChanges();
           return RedirectToAction("GioHang");
		}
        public IActionResult Congsp(int layid)
        {
            var cong = _db.gioHangs.FirstOrDefault(sp => sp.Id == layid);
            cong.Quanlity += 1;
            if (cong.Quanlity == 0)
            {
                _db.gioHangs.Remove(cong);
            }
            _db.SaveChanges();
             return RedirectToAction("GioHang");
        }
        [Authorize]
		public IActionResult giohangid(int sanphamId)
		{
           
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var claims = identity.FindFirst(ClaimTypes.Name);

            var giohang = _db.gioHangs
				.FirstOrDefault(gh => gh.AppUserId == claim.Value && gh.sanphamId == sanphamId);
         
            if (giohang == null)
            {
             
                giohang = new GioHang
                {
                    sanphamId = sanphamId,
                    AppUserId = claim.Value,
                    Quanlity = 1,
                    Username = claims.Value,
                 
                   
                    
                };

                // Thêm giỏ hàng mới vào danh sách.
                _db.gioHangs.Add(giohang);
                _db.SaveChanges();
                return RedirectToAction("GioHang");
            }
            else
            {
                TempData["Message"] = "Sản phẩm này đã tồn tại trong giỏ hàng.";
                return RedirectToAction("GioHang"); // Chuyển hướng đến trang index hoặc trang bạn muốn hiển thị giỏ hàng.

            }
			}
        [HttpGet]
        public IActionResult Thanhtoan()
        {
       
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
      
            GioHangViewModel giohang = new GioHangViewModel
            {
                dsgioHang = _db.gioHangs
              .Include(gh => gh.sanphams)
              .Where(gh => gh.AppUserId == claim.Value)
              .ToList(),
                hoaDons = new HoaDon()
        };
    
        
        
            foreach (var item in giohang.dsgioHang)
            {
                item.giatien = item.Quanlity * Convert.ToInt32(item.sanphams.Gia);
                giohang.hoaDons.Total += item.giatien;
            }
        

            return View(giohang);
        }
        [HttpPost]
        public IActionResult Thanhtoan(GioHangViewModel gh)
        {

            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            gh.dsgioHang = _db.gioHangs.Include(h => h.sanphams).Where(sp => sp.AppUserId == claim.Value).ToList();
           
            foreach (var item in gh.dsgioHang)
            {
                item.giatien = item.Quanlity * Convert.ToInt32(item.sanphams.Gia);
                gh.hoaDons.Total += item.giatien;
            }
            gh.hoaDons.ApplicationUserId = claim.Value;
            gh.hoaDons.OrderDate = DateTime.Now;
            _db.hoaDons.Add(gh.hoaDons);
            _db.SaveChanges();
            foreach ( var item in gh.dsgioHang)
            { 
                ChitietHoaDon ct = new ChitietHoaDon();
                {
                    ct.HoaDonId = gh.hoaDons.Id;
                    ct.Quality = item.Quanlity;
                    ct.Price = item.giatien;
                    ct.sanphamId = item.sanphamId;
                }
                _db.chitietHoaDons.Add(ct);
                _db.SaveChanges();
            }

       

            _db.gioHangs.RemoveRange(gh.dsgioHang);
            return View(gh);
        }
        [HttpPost]
       
        public IActionResult DatBan(DatBan db)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
         

            
                db.UserId = claim.Value;
                _db.datBans.Add(db);
                _db.SaveChanges();
            
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult DatBan()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SpTimkiem(string search)
        {
            if (search != null)
            {
                // Sử dụng LINQ để thực hiện tìm kiếm không phân biệt chữ hoa chữ thường
                // Sử dụng LINQ để thực hiện tìm kiếm không phân biệt chữ hoa chữ thường
                List<Sanpham> danhSachSanPham = _db.sanphams.Include("nhacungcaps")
           .Where(sp => sp.Name.ToLower().Contains(search.ToLower()))
           .ToList();

                return View(danhSachSanPham);
            }
            else
            {
             return     RedirectToAction("Index", "Home");
			}
         
        }
    }

}
