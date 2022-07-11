using DataStore.AbstractEntities;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataStore.Entities
{
    public class RemindEntity : BaseEntity
    {
        /// <summary>
        /// Пользователь, которому принадлежит напоминание
        /// </summary>
        public UserEntity User { get; set; }

        /// <summary>
        /// Текст заметки
        /// </summary>
        [StringLength(2000)]
        public string Text { get; set; }
        /// <summary>
        /// Дата напоминания
        /// </summary>
        public DateTime RemindDate { get; set; }
        /// <summary>
        /// Пометка об удалении напоминания
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Пометка об отправке напоминания
        /// </summary>
        public bool IsInvoked { get; set; }
    }
}
