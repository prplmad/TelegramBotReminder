using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class User
    {
        public int Id { get; set; }
        /// <summary>
        /// Состояние пользователя на данный момент
        /// </summary>
        public State State { get; set; }
        public List<Note> Notes { get; set; }
        public List<Remind> Reminds { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
