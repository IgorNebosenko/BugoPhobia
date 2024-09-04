using System.Collections.Generic;

namespace ElectrumGames.Core.Journal
{
    public class JournalManager
    {
        public JournalInstance PlayerJournalInstance { get; private set; }
        
        public List<JournalInstance> OtherPlayersJournalInstances { get; private set; }

        public JournalManager()
        {
            PlayerJournalInstance = new JournalInstance();
            OtherPlayersJournalInstances = new List<JournalInstance>();
        }
    }
}
