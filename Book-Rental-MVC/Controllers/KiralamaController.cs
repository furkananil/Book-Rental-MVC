using Book_Rental_MVC.Models;
using Book_Rental_MVC.Models.Abstract;
using Book_Rental_MVC.Models.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace Book_Rental_MVC.Controllers
{
    public class KiralamaController : Controller
    {
        private readonly IKiralamaRepository _repository;
        private readonly IKitapRepository _kitapRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = kiralamaRepository;
            _kitapRepository = kitapRepository; 
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Kiralama> kiralamaList = _repository.GetAll(includeProps:"Kitap").ToList();
            return View(kiralamaList);
        }

        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
                .Select(k => new SelectListItem { Text = k.KitapAdi, Value = k.Id.ToString() });

            ViewBag.KitapList = KitapList;

            if (id == null || id == 0)
                return View();
            else
            {
                Kiralama? kiralamaVt = _repository.Get(i => i.Id == id); // idye ait kaydi getir

                if (kiralamaVt == null)
                    return NotFound();

                return View(kiralamaVt);
            }
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama model)
        {
            if(ModelState.IsValid) 
            {
                if (model.Id == 0)
                {
                    _repository.Ekle(model);
                    TempData["SuccessMessage"] = "Yeni Kiralama Kaydı Oluşturuldu";
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

        public IActionResult Sil(int? id)
        {
            if(id == null || id == 0)
                return NotFound();

            Kiralama? kiralamaVt = _repository.Get(i => i.Id == id); // idye ait kaydi getir

            if (kiralamaVt == null)
                return NotFound();

            return View(kiralamaVt);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPost(int? id)
        {
            Kiralama? kiralama = _repository.Get(i => i.Id == id);

            if (kiralama == null)
            {
                TempData["ErrorMessage"] = "Silmek istediğiniz kayıt bulunamadı!";
                return NotFound();
            }

            _repository.Sil(kiralama);
            _repository.Kaydet();
            TempData["SuccessMessage"] = "Kiralama kaydı başarıyla silindi!";

            return RedirectToAction("Index");
        }
    }
}
