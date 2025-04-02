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
            return Kaydet(model, false); // Ekleme işlemi
        }

        public IActionResult Guncelle(int? id)
        {
            return ContextBulVeDon(id);
        }

        [HttpPost]
        public IActionResult Guncelle(KitapTuru model)
        {
            return Kaydet(model, true); // Güncelleme işlemi
        }

        public IActionResult Sil(int? id)
        {
            return ContextBulVeDon(id);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPost(int? id)
        {
            KitapTuru? kitapTuru = _context.KitapTurleri.Find(id);

            if (kitapTuru == null)
            {
                TempData["ErrorMessage"] = "Silmek istediğiniz kayıt bulunamadı!";
                return NotFound();
            }

            _context.KitapTurleri.Remove(kitapTuru);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Kitap türü başarıyla silindi!";

            return RedirectToAction("Index");
        }

        private IActionResult ContextBulVeDon(int? id)
        {
            if (id == null)
                return NotFound();

            KitapTuru? kitapTuruVt = _context.KitapTurleri.Find(id); // idye ait kaydi getir

            if (kitapTuruVt == null)
                return NotFound();

            return View(kitapTuruVt);
        }

        private IActionResult Kaydet(KitapTuru model, bool isUpdate)
        {
            if (ModelState.IsValid)
            {
                if (isUpdate)
                {
                    _context.KitapTurleri.Update(model);
                    TempData["SuccessMessage"] = "Kitap türü başarıyla güncellendi!";
                }
                else
                {
                    _context.KitapTurleri.Add(model);
                    TempData["SuccessMessage"] = "Kitap türü başarıyla eklendi!";
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "İşlem sırasında bir hata oluştu!";
            return View(model);
        }
    }
}
