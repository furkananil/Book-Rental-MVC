using Book_Rental_MVC.Models.Abstract;
using Book_Rental_MVC.Utility;
using System.Linq.Expressions;

namespace Book_Rental_MVC.Models.Concrete
{
    public class KitapTuruRepository : Repository<KitapTuru>, IKitapTuruRepository
    {
        private AppDbContext _context;
        public KitapTuruRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public void Guncelle(KitapTuru kitapTuru)
        {
            _context.Update(kitapTuru);
        }

        public void Kaydet()
        {
            _context.SaveChanges();
        }
    }
}
