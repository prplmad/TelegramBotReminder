using System;
using System.ComponentModel.DataAnnotations;

namespace DataStore.AbstractEntities
{
    public abstract class BaseEntity : IBaseEntityId, IBaseEntityCreatedAt
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public abstract class BaseEntity2 : IBaseEntityId, IBaseEntityUpdatedAt
    {
        [Key]
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
