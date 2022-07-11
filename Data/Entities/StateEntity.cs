using DataStore.AbstractEntities;

namespace DataStore.Entities
{
    public enum TelegramState
    {
        None = 0,
        Note = 1,
        Remind = 2,
        SetDate = 3,
        DeleteNote = 4,
        DeleteRemind = 5
    }
    public class StateEntity : BaseEntity2
    {
        public int UserId { get; set; }
        public TelegramState TelegramState { get; set; }

    }
}
