using Book_Rental_MVC.Models.Abstract;
using Book_Rental_MVC.Utility;

namespace Book_Rental_MVC.Models.Concrete
{
    public class KiralamaRepository : Repository<Kiralama>, IKiralamaRepository
    {
        private AppDbContext _context;
        public KiralamaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public void Guncelle(Kiralama kiralama)
        {
            _context.Update(kiralama);
        }

        public void Kaydet()
        {
            _context.SaveChanges();
        }
    }
}
