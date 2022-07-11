using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.AbstractEntities
{
    internal interface IBaseEntityCreatedAt
    {
        DateTime CreatedAt { get; set; }
    }
}
