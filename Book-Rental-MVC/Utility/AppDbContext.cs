using Book_Rental_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Rental_MVC.Utility
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<KitapTuru> KitapTurleri { get; set; }
        public DbSet<Kitap> Kitaplar { get; set; }
    }
}
