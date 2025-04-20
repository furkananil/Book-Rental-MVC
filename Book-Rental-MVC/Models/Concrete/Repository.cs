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
            _context.Kitaplar.Include(k => k.KitapTuru).Include(k => k.KitapTuruId);
        }
        public void Ekle(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filtre);

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            
            if(!string.IsNullOrEmpty(includeProps))
            {
                foreach(var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.ToList();
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
