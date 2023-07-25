using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace BorrowMe.Models
{
    public class Borrowing
    {
        public int Id { get; set; }
        
        [Required]
        public int BorrowerId { get; set; }
        
        [Required]
        public int ItemId { get; set; }
        
        [Required]
        public int ItemType { get; set; }
        
        [Required]
        public DateTime TakeDate { get; set; }
        
        [Required]
        public DateTime ReturnDate { get; set; }
        
        [Required]
        public bool IsReserved { get; set; }

        public Borrowing()
        {
            IsReserved = false;
        }
       
    }
}
