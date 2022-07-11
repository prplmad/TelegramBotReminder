using System;

namespace Business.Models
{
    public class Remind
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime RemindDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Статус отправки напоминания
        /// </summary>
        public bool IsInvoked { get; set; }
    }
}
