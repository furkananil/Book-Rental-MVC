using Microsoft.EntityFrameworkCore;

namespace Book_Rental_MVC.Utility
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
