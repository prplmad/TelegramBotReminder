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
        public async Task<bool> AddNote(User user, string text)
        {
            Note note = new();
            note.Text = text;
            note.UserId = user.Id;
            note.CreatedAt = DateTime.Now;
            await _notesRepository.AddNote(note);
            user.State = State.None;
            await _statesRepository.UpdateState(user);
            return true;
        }


        public async Task<IReadOnlyCollection<Note>> GetNotes(User user)
        {
            var listOfNotes = await _notesRepository.GetNotes(user);
            return listOfNotes;
        }

        public async Task<bool> DeleteNote(User user, int noteid)
        {
            Note note = new();
            note.Id = noteid;
            if (await _notesRepository.DeleteNote(note) == true)
            {
                user.State = State.None;
                await _statesRepository.UpdateState(user);
                return true;
            }
            else
            {
                user.State = State.None;
                await _statesRepository.UpdateState(user);
                return false;
            }
        }
    }
}
