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
            else
            {
                return View(model);
            }
        }
    }
}
