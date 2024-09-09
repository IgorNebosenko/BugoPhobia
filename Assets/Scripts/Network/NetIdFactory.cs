using System.Collections.Generic;
using ElectrumGames.CommonInterfaces;

namespace ElectrumGames.Network
{
    public class NetIdFactory
    {
        private int _lastNetId = 0;
        
        private HashSet<int> _aliveIds = new();

        public IHaveNetId Initialize(IHaveNetId networkObject, int ownerId = -1)
        {
            _aliveIds.Add(++_lastNetId);
            networkObject.SetNetId(_lastNetId, ownerId);

            return networkObject;
        }
    }
}