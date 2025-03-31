using System.ComponentModel.DataAnnotations;

namespace Book_Rental_MVC.Models
{
    public class KitapTuru
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ad { get; set; }
    }
}
