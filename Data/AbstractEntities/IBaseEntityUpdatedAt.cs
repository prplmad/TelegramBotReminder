using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.AbstractEntities
{
    public interface IBaseEntityUpdatedAt
    {
        DateTime UpdatedAt { get; set; }
    }
}
