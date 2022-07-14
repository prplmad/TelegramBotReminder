using System.Threading.Tasks;
using System.Collections.Generic;
using Business.Models;
using System.Threading;

namespace Business.Abstract.Repositories
{
    public interface INotesRepository
    {
        Task<bool> AddNoteAsync(Note note, CancellationToken ct = default);
        Task<bool> DeleteNoteAsync(Note note, CancellationToken ct = default);
        Task<IReadOnlyCollection<Note>> GetNotesAsync(User user, CancellationToken ct = default);
    }
}
