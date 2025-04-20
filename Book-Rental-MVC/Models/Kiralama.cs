using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Rental_MVC.Models
{
    public class Kiralama
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OgrenciId { get; set; }

        [ValidateNever]
        public int KitapId { get; set; }
        [ForeignKey(nameof(KitapId))]

        [ValidateNever]
        public Kitap Kitap { get; set; }
    }
}
