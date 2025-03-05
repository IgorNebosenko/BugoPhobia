using System.Collections.Generic;
using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Lobby
{
    public class LobbyManager
    {
        private const string LobbyIdKey = "LobbyId";
        private const string OpenedLobbiesKey = "OpenedLobbies";
        
        public int LobbyId
        {
            get => PlayerPrefsAes.GetEncryptedInt(LobbyIdKey);
            private set => PlayerPrefsAes.SetEncryptedInt(LobbyIdKey, value);
        }

        public LobbyManager()
        {
            if (OpenedLobbies() == null || OpenedLobbies().Length == 0)
                AddOpenedLobby(0);
        }

        public int[] OpenedLobbies()
        {
            return JsonUtility.FromJson<int[]>(PlayerPrefsAes.GetEncryptedString(OpenedLobbiesKey));
        }

        public void AddOpenedLobby(int lobbyId)
        {
            var openedLobbies = new List<int>();
            var rangeLobbies = OpenedLobbies();
            
            if (rangeLobbies != null)
                openedLobbies.AddRange(rangeLobbies);
            
            if (openedLobbies.Contains(lobbyId))
                return;
            
            openedLobbies.Add(lobbyId);
            PlayerPrefsAes.GetEncryptedString(OpenedLobbiesKey, JsonUtility.ToJson(openedLobbies.ToArray()));
        }
    }
}