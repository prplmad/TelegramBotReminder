
namespace NoteBot.Mappers
{
    public static class RemindMapper
    {

        public static Models.Remind FromBusinessToAPI(this Business.Models.Remind remind)
        {
            return new Models.Remind
            {
                RemindDate = remind.RemindDate,
                CreatedAt = remind.CreatedAt,
                RemindId = remind.Id,
                Text = remind.Text
            };
        }
    }
}
