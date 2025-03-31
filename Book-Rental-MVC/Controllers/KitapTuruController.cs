using Microsoft.AspNetCore.Mvc;

namespace Book_Rental_MVC.Controllers
{
    public class KitapTuruController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
