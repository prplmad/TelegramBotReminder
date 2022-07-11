using DataStore.AbstractEntities;
using System.ComponentModel.DataAnnotations;

namespace DataStore.Entities
{
    public class NoteEntity : BaseEntity
    {
        /// <summary>
        /// Текст заметки
        /// </summary>
        [StringLength(2000)]
        public string Text { get; set; }

        public UserEntity User { get; set; }

        /// <summary>
        /// Пометка об удалении заметки
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
