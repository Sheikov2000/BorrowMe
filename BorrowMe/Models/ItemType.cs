


using System.ComponentModel.DataAnnotations;

namespace BorrowMe.Models
{
    public class ItemType
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

    }
}
