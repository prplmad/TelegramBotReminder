using Business.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Business.Abstract.Services
{
    public interface INotesService
    {
        Task<bool> AddNote(User user, string text);
        Task<bool> DeleteNote(User user, int noteid);
        Task<IReadOnlyCollection<Note>> GetNotes(User user);
    }
}
