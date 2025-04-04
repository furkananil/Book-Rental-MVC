using System.Linq.Expressions;

namespace Book_Rental_MVC.Models.Abstract
{
    public interface IRepository<T> where T : class
    {
        // T => KitapTuru
        
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filtre);
        void Ekle(T entity);
        void Sil(T entity);
        void SilAralik(IEnumerable<T> entities);
    }
}
