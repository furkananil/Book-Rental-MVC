using Book_Rental_MVC.Models;
using Book_Rental_MVC.Models.Abstract;
using Book_Rental_MVC.Models.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_Rental_MVC.Controllers
{
    public class KitapController : Controller
    {
        private readonly IKitapRepository _repository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Kitap> kitapList = _repository.GetAll().ToList();
            return View(kitapList);
        }

        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll()
                .Select(k => new SelectListItem { Text = k.Ad, Value = k.Id.ToString() });

            ViewBag.KitapTuruList = KitapTuruList;

            if (id == null || id == 0)
                return View();
            else
                return ContextBulVeDon(id);
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kitap model, IFormFile? file)
        {
            if(ModelState.IsValid) 
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");

                using(var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                model.ResimUrl = @"\img\" + file.FileName;

                _repository.Ekle(model);
                _repository.Kaydet();
                TempData["SuccessMessage"] = "Yeni Kitap Oluşturuldu";
                return RedirectToAction("Index");
            }
            return View();
        }

        /*
        public IActionResult Guncelle(int? id)
        {
            return ContextBulVeDon(id);
        }
        

        [HttpPost]
        public IActionResult Guncelle(Kitap model)
        {
            return Kaydet(model, true); // Güncelleme işlemi
        }
        */

        public IActionResult Sil(int? id)
        {
            return ContextBulVeDon(id);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPost(int? id)
        {
            Kitap? kitap = _repository.Get(i => i.Id == id);

            if (kitap == null)
            {
                TempData["ErrorMessage"] = "Silmek istediğiniz kayıt bulunamadı!";
                return NotFound();
            }

            _repository.Sil(kitap);
            _repository.Kaydet();
            TempData["SuccessMessage"] = "Kitap başarıyla silindi!";

            return RedirectToAction("Index");
        }

        private IActionResult ContextBulVeDon(int? id)
        {
            if (id == null)
                return NotFound();

            Kitap? kitapVt = _repository.Get(i => i.Id == id); // idye ait kaydi getir

            if (kitapVt == null)
                return NotFound();

            return View(kitapVt);
        }

        private IActionResult Kaydet(Kitap model, bool isUpdate)
        {
            if (ModelState.IsValid)
            {
                if (isUpdate)
                {
                    _repository.Guncelle(model);
                    TempData["SuccessMessage"] = "Kitap başarıyla güncellendi!";
                }
                else
                {
                    _repository.Ekle(model);
                    TempData["SuccessMessage"] = "Kitap başarıyla eklendi!";
                }

                _repository.Kaydet();
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "İşlem sırasında bir hata oluştu!";
            return View(model);
        }
    }
}
