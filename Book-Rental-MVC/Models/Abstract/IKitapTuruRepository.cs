namespace Book_Rental_MVC.Models.Abstract
{
    public interface IKitapTuruRepository : IRepository<KitapTuru>
    {
        void Guncelle(KitapTuru kitapTuru);
        void Kaydet();
    }
}
