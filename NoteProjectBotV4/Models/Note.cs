using System;

namespace NoteBot.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
