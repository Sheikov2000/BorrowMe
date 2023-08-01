
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BorrowMe.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }

        public List<Accessory> Accessories { get; set; }

        User User { get; set; } 

    }

}
