using Book_Rental_MVC.Models;
using Book_Rental_MVC.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Book_Rental_MVC.Controllers
{
    public class KitapTuruController : Controller
    {
        private readonly AppDbContext _context;
        public KitapTuruController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<KitapTuru> kitapTuruList = _context.KitapTurleri.ToList();
            return View(kitapTuruList);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(KitapTuru model)
        {
            if(ModelState.IsValid)
            {
                _context.KitapTurleri.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Guncelle(int? id)
        {
            if(id == null)
                return NotFound();

            KitapTuru? kitapTuruVt = _context.KitapTurleri.Find(id); // idye ait kaydi getir

            if(kitapTuruVt == null)
                return NotFound();

            return View(kitapTuruVt);
        }

        [HttpPost]
        public IActionResult Guncelle(KitapTuru model)
        {
            if (ModelState.IsValid)
            {
                _context.KitapTurleri.Update(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
