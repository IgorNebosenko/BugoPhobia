using ElectrumGames.Core.Missions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class ToLobbyZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadSceneAsync((int)MissionMap.LobbyTier0);
        }
    }
}