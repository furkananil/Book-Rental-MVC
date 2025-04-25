using Book_Rental_MVC.Models;
using Book_Rental_MVC.Models.Abstract;
using Book_Rental_MVC.Models.Concrete;
using Book_Rental_MVC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

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

        [Authorize(Roles = "Admin,Ogrenci")]
        public IActionResult Index()
        {
            List<Kitap> kitapList = _repository.GetAll(includeProps:"KitapTuru").ToList();
            return View(kitapList);
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
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

        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
        public IActionResult EkleGuncelle(Kitap model, IFormFile? file)
        {
            if(ModelState.IsValid) 
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");

                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.ResimUrl = @"\img\" + file.FileName;
                }
              
                if (model.Id == 0)
                {
                    _repository.Ekle(model);
                    TempData["SuccessMessage"] = "Yeni Kitap Oluşturuldu";
                }
                else
                {
                    _repository.Guncelle(model);
                    TempData["SuccessMessage"] = "Yeni Güncelleme Başarılı";
                }
                _repository.Kaydet();
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {
            return ContextBulVeDon(id);
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
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
    }
}
