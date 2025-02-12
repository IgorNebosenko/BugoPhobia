using UnityEngine;

namespace ElectrumGames.Core.Environment
{
    public class DoorTriggerObject : MonoBehaviour
    {
        [SerializeField] private DoorEnvironmentObject doorEnvironmentObject;
        
        public void SetCameraAngleAndInteract(Vector2 inputLook)
        {
            doorEnvironmentObject.SetCameraAngleAndInteract(inputLook);
        }
    }
}