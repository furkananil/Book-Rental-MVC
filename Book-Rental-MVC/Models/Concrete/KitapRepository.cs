using Book_Rental_MVC.Models.Abstract;
using Book_Rental_MVC.Utility;

namespace Book_Rental_MVC.Models.Concrete
{
    public class KitapRepository : Repository<Kitap>, IKitapRepository
    {
        private AppDbContext _context;
        public KitapRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public void Guncelle(Kitap kitap)
        {
            _context.Update(kitap);
        }

        public void Kaydet()
        {
            _context.SaveChanges();
        }
    }
}
