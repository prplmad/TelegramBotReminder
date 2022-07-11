using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBot.Models
{
    public enum State
    {
        None = 0,
        Note = 1,
        Remind = 2,
        SetDate = 3,
        DeleteNote = 4,
        DeleteRemind = 5
    }
}
