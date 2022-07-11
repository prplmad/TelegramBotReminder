

namespace DataStore.Mappers
{
    public static class RemindMapper
    {
        public static DataStore.Entities.RemindEntity FromBusinessToEntities(this Business.Models.Remind remind)
        {
            return new DataStore.Entities.RemindEntity
            {
                Id = remind.Id,
                Text = remind.Text,
                CreatedAt = remind.CreatedAt,
                RemindDate = remind.RemindDate
            };
        }

        public static Business.Models.Remind FromEntitiesToBusiness(this DataStore.Entities.RemindEntity remind)
        {
            return new Business.Models.Remind
            {
                RemindDate = remind.RemindDate,
                CreatedAt = remind.CreatedAt,
                Id = remind.Id,
                Text = remind.Text
            };
        }
    }
}
