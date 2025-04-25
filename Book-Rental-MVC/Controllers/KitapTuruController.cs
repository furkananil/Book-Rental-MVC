using Book_Rental_MVC.Models;
using Book_Rental_MVC.Models.Abstract;
using Book_Rental_MVC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Rental_MVC.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KitapTuruController : Controller
    {
        private readonly IKitapTuruRepository _repository;
        public KitapTuruController(IKitapTuruRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            List<KitapTuru> kitapTuruList = _repository.GetAll().ToList();
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
            KitapTuru? kitapTuru = _repository.Get(i => i.Id == id);

            if (kitapTuru == null)
            {
                TempData["ErrorMessage"] = "Silmek istediğiniz kayıt bulunamadı!";
                return NotFound();
            }

            _repository.Sil(kitapTuru);
            _repository.Kaydet();
            TempData["SuccessMessage"] = "Kitap türü başarıyla silindi!";

            return RedirectToAction("Index");
        }

        private IActionResult ContextBulVeDon(int? id)
        {
            if (id == null)
                return NotFound();

            KitapTuru? kitapTuruVt = _repository.Get(i => i.Id == id); // idye ait kaydi getir

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
                    _repository.Guncelle(model);
                    TempData["SuccessMessage"] = "Kitap türü başarıyla güncellendi!";
                }
                else
                {
                    _repository.Ekle(model);
                    TempData["SuccessMessage"] = "Kitap türü başarıyla eklendi!";
                }

                _repository.Kaydet();
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "İşlem sırasında bir hata oluştu!";
            return View(model);
        }
    }
}
