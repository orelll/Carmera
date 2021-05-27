using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signaling_server.Socketing.Repositories
{
    public class ICERepository : IICERepository
    {
        private Dictionary<int, object> _iceServers;

        public ICERepository() {
            _iceServers = new Dictionary<int, object>();
        }

        public void AddEntry(object iceData)
        {
            var hash = iceData.GetHashCode();
            if (_iceServers.ContainsKey(hash))
            {
                _iceServers.Add(hash, iceData);
            }
        }

        public IEnumerable<object> GetAll() => _iceServers.Select(ice => ice.Value);

        public void RemoveAll() => _iceServers.Clear();

        public void RemoveEntry(object iceData) => _iceServers.Remove(iceData.GetHashCode(), out var x);
    }
}
