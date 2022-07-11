
namespace DataStore.Mappers
{
    public static class UserMapper
    {
        public static DataStore.Entities.UserEntity FromBusinessToEntities(this Business.Models.User user)
        {
            return new DataStore.Entities.UserEntity
            {
                UserId = user.Id,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
