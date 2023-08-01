


using System.ComponentModel.DataAnnotations;

namespace BorrowMe.Models
{
    public class Accessory
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        public int ItemId { get; set; } 

        public string Details { get; set; }

    }
}
