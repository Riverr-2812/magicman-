using Microsoft.AspNetCore.Mvc;
using Web_CuoiKy.Data;
using Web_CuoiKy.Models;

namespace Web_CuoiKy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhaCungCapController : Controller
    {

        private readonly ApplicationDbContext _dbs;

        public NhaCungCapController(ApplicationDbContext dbs)
        {
            _dbs = dbs;

        }

        public IActionResult Index()
        {

            var nhacungcap = _dbs.nhacungcaps.ToList();
            ViewBag.NhaCungCap = nhacungcap;

            return View();


        }

        [HttpGet]
        public IActionResult Creates()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Creates(Nhacungcap nhacungcap)

        {
            if (ModelState.IsValid)
            {


                _dbs.nhacungcaps.Add(nhacungcap);
                _dbs.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var nhacungcap = _dbs.nhacungcaps.Find(id);
            return View(nhacungcap);
        }
        [HttpPost]
        public IActionResult Edit(Nhacungcap nhacungcap)

        {
            if (ModelState.IsValid)
            {


                _dbs.nhacungcaps.Update(nhacungcap);
                _dbs.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }
            var nhacungcap = _dbs.nhacungcaps.Find(Id);
            return View(nhacungcap);

        }
        [HttpPost]
        public IActionResult Deleteconfirm(int Id)

        {
            var nhacungcap = _dbs.nhacungcaps.Find(Id);
            if (nhacungcap == null)
            {
                return NotFound();
            }
            _dbs.nhacungcaps.Remove(nhacungcap);
            _dbs.SaveChanges();
            return RedirectToAction("Index");

           

        }
        
    }
}
