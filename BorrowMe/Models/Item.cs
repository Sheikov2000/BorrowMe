
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BorrowMe.Models
{
    public class Item
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }


    }

}
