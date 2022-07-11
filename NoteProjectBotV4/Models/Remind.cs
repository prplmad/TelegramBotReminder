using System;

namespace NoteBot.Models
{
    public class Remind
    {
        public int RemindId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime RemindDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        //сообщение отправлено
        public bool IsInvoked { get; set; }
    }
}
