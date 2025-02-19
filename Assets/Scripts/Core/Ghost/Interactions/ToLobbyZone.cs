using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class ToLobbyZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerBase>(out var _))
                SceneManager.LoadSceneAsync((int)MissionMap.LobbyTier0);
        }
    }
}