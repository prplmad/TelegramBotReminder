

namespace NoteBot.Mappers
{
    public static class UserMapper
    {
        public static Business.Models.User FromApiToBusiness(this Telegram.Bot.Types.User user)
        {
            return new Business.Models.User
            {
                Id = (int)user.Id
            };
        }
    }
}
