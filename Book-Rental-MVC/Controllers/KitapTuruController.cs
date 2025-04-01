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
    }
}
