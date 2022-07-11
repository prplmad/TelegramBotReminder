

namespace NoteBot.Mappers
{
    public static class NoteMapper
    {

        public static Models.Note FromBusinessToAPI(this Business.Models.Note note)
        {
            return new Models.Note
            {
                CreatedAt = note.CreatedAt,
                NoteId = note.Id,
                Text = note.Text
            };
        }
    }
}
