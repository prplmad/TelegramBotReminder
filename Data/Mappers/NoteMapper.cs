

namespace DataStore.Mappers
{
    public static class NoteMapper
    {
        public static DataStore.Entities.NoteEntity FromBusinessToEntities(this Business.Models.Note note, Business.Models.User user)
        {
            return new DataStore.Entities.NoteEntity
            {
                Id = note.Id,
                User = user.FromBusinessToEntities(),
                Text = note.Text,
                CreatedAt = note.CreatedAt,
            };
        }

        public static DataStore.Entities.NoteEntity FromBusinessToEntities(this Business.Models.Note note)
        {
            return new DataStore.Entities.NoteEntity
            {
                Id = note.Id,
                Text = note.Text,
                CreatedAt = note.CreatedAt,
            };
        }

        public static Business.Models.Note FromEntitiesToBusiness(this DataStore.Entities.NoteEntity note)
        {
            return new Business.Models.Note
            {
                CreatedAt = note.CreatedAt,
                Id = note.Id,
                Text = note.Text
            };
        }
    }
}
