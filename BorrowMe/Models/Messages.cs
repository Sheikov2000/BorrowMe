using System;

namespace BorrowMe.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string Text { get; set; }

        public Boolean isRead { get; set; }

    }
}
