using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signaling_server.Socketing.Repositories
{
    public interface IICERepository
    {
        void AddEntry(object iceData);
        IEnumerable<object> GetAll();
        void RemoveEntry(object iceData);
        void RemoveAll();
    }
}
