using Business.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Business.Abstract.Services
{
    public interface INotesService
    {
        Task<bool> AddNoteAsync(User user, string text);
        Task<bool> DeleteNoteAsync(User user, int noteid);
        Task<IReadOnlyCollection<Note>> GetNotesAsync(User user);
    }
}
