using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web_CuoiKy.Data;
using Web_CuoiKy.Models;

namespace Web_CuoiKy.View_Components
{
    public class NhacungcapViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
   
        public NhacungcapViewComponent(ApplicationDbContext db) => _db = db;

        [Authorize]
        public IViewComponentResult Invoke()
        {
        
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim?.Value;
      
            if (identity.IsAuthenticated)
            {


                GioHangViewModel giohang = new GioHangViewModel
                {
                    dsgioHang = _db.gioHangs
                   .Include(gh => gh.sanphams)
                   .Where(gh => gh.AppUserId == userId)
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
            else
            {
                return View("Default", new List<Sanpham>());


            }



        }
    }
}
