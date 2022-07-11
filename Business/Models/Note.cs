using System;

namespace Business.Models
{
    public class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
