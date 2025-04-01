using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Book_Rental_MVC.Models
{
    public class KitapTuru
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Kitap Türü Adı")]
        public string Ad { get; set; }
    }
}
