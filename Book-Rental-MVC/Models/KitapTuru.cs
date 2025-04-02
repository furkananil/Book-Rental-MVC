using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Book_Rental_MVC.Models
{
    public class KitapTuru
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitap Türü Adı boş bırakılamaz!")]
        [MaxLength(25)]
        [DisplayName("Kitap Türü Adı")]
        public string Ad { get; set; }
    }
}
