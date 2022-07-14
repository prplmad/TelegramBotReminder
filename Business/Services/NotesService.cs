using System;
using Business.Abstract.Repositories;
using Business.Abstract.Services;
using System.Threading.Tasks;
using Business.Models;
using System.Collections.Generic;

namespace Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly IStatesRepository _statesRepository;
        public NotesService(INotesRepository notesRepository, IStatesRepository statesRepository)
        {
            _notesRepository = notesRepository;
            _statesRepository = statesRepository;
        }
        public async Task<bool> AddNoteAsync(User user, string text)
        {
            Note note = new();
            note.Text = text;
            note.UserId = user.Id;
            note.CreatedAt = DateTime.Now;
            await _notesRepository.AddNoteAsync(note);
            user.State = State.None;
            await _statesRepository.UpdateStateAsync(user);
            return true;
        }


        public async Task<IReadOnlyCollection<Note>> GetNotesAsync(User user)
        {
            var listOfNotes = await _notesRepository.GetNotesAsync(user);
            return listOfNotes;
        }

        public async Task<bool> DeleteNoteAsync(User user, int noteid)
        {
            Note note = new();
            note.Id = noteid;
            if (await _notesRepository.DeleteNoteAsync(note) == true)
            {
                user.State = State.None;
                await _statesRepository.UpdateStateAsync(user);
                return true;
            }
            else
            {
                user.State = State.None;
                await _statesRepository.UpdateStateAsync(user);
                return false;
            }
        }
    }
}
