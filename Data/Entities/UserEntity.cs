using DataStore.AbstractEntities;
using System.Collections.Generic;
using DataStore;


namespace DataStore.Entities
{
    public class UserEntity : BaseEntity
    {
        public int UserId { get; set; }

        /// <summary>
        /// Заметки
        /// </summary>
        public List<NoteEntity> Notes { get; set; }

        /// <summary>
        /// Напоминания
        /// </summary>
        public List<RemindEntity> Reminds { get; set; }

        public UserEntity()
        {
            Notes = new List<NoteEntity>();
            Reminds = new List<RemindEntity>();
        }


    }
}
