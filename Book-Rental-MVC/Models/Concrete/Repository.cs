using Book_Rental_MVC.Models.Abstract;
using Book_Rental_MVC.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Book_Rental_MVC.Models.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        internal DbSet<T> dbSet; 

        public Repository(AppDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>(); // _context.KitapTurleri
        }
        public void Ekle(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filtre)
        {
            return dbSet.FirstOrDefault(filtre);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public void Sil(T entity)
        {
            dbSet.Remove(entity);
        }

        public void SilAralik(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
