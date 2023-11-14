using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Web_CuoiKy.Data;
using Web_CuoiKy.Models;

namespace Web_CuoiKy.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Sanpham> sp = _db.sanphams.Include("nhacungcaps").ToList();

            // Kiểm tra xem có thông báo nào trong TempData.



            return View(sp);

		}
		[HttpGet]
		public IActionResult Details(int SanphamId)
		{
			GioHang giohang = new GioHang()
			{
				sanphamId = SanphamId,
				sanphams = _db.sanphams.Include(sp => sp.nhacungcaps).FirstOrDefault(sp => sp.Id == SanphamId),
				Quanlity = 1

			};

			return View(giohang);
		}
		[HttpPost]
		[Authorize]
		public IActionResult Details(GioHang giohang)
		{
			var identity = (ClaimsIdentity)User.Identity;
			var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
			giohang.AppUserId = claim.Value;
			_db.gioHangs.Add(giohang);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}