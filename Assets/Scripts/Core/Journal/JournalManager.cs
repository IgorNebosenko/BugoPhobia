using System.Collections.Generic;

namespace ElectrumGames.Core.Journal
{
    public class JournalManager
    {
        public JournalInstance PlayerJournalInstance { get; private set; }
        
        public List<JournalInstance> OtherPlayersJournalInstances { get; private set; }
        
        
    }
}
