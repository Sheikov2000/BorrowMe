using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace BorrowMe.Models
{
    public class Messages
    {
        public int Id { get; set; }

        [Required]
        public int SenderId { get; set; }
        public User Sender { get; set; }
        [Required]
        public int RecipientId { get; set; }
        public User Recipient { get; set; }

        [Required]
        [MaxLength (250)]
        public string Text { get; set; }
        
        [Required]
        public bool IsRead { get; set; }

        public Messages()
        {
            IsRead = false;
        }
    }
}
