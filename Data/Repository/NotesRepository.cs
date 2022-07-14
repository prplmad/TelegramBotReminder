using Business.Abstract.Repositories;
using DataStore.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Business.Models;
using System.Collections.Generic;
using DataStore.Mappers;
using System.Threading;

namespace DataStore.Repository
{
    public class NotesRepository : INotesRepository
    {
        private readonly ApplicationContext _db;
        public NotesRepository(ApplicationContext db)
        {
            _db = db;
        }
        public async Task<bool> AddNoteAsync(Note note, CancellationToken ct = default)
        {
            NoteEntity noteEntity = note.FromBusinessToEntities();
            var UserId = note.UserId;
            noteEntity.User = await _db.Users.SingleOrDefaultAsync(x => x.UserId == UserId);
            await _db.AddAsync(noteEntity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyCollection<Note>> GetNotesAsync(User user, CancellationToken ct = default)
        {
            await Task.Delay(0);
            UserEntity userEntity = user.FromBusinessToEntities();
            NoteEntity[] notes = _db.Notes.Where(p => p.User.UserId == userEntity.UserId).Where(p => p.IsDeleted != true).ToArray();
            List<Note> listOfNotes = new();
            foreach (var note in notes)
            {
                listOfNotes.Add(note.FromEntitiesToBusiness());
            }
            return listOfNotes;
        }

        public async Task<bool> DeleteNoteAsync(Note note, CancellationToken ct = default)
        {
            NoteEntity noteEntity = note.FromBusinessToEntities();
            NoteEntity _note = await _db.Notes.FindAsync(noteEntity.Id);
            if (_note != null && _note.IsDeleted is not true)
            {
                _note.IsDeleted = true;
                _db.Notes.Update(_note);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
