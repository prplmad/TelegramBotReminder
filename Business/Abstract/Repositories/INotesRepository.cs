using System.Threading.Tasks;
using System.Collections.Generic;
using Business.Models;
using System.Threading;

namespace Business.Abstract.Repositories
{
    public interface INotesRepository
    {
        Task<bool> AddNote(Note note, CancellationToken ct = default);
        Task<bool> DeleteNote(Note note, CancellationToken ct = default);
        Task<IReadOnlyCollection<Note>> GetNotes(User user, CancellationToken ct = default);
    }
}
