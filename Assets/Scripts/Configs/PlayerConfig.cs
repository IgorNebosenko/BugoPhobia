using UnityEngine;

namespace ElectrumGames.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Entities/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float DefaultSpeed { get; private set; }
        [field: SerializeField] public float RunSpeed { get; private set; }
        [field: SerializeField] public float RunDuration { get; private set; }
        [field: SerializeField] public float RunCooldown { get; private set; }
        [field: SerializeField] public float SitStandDuration { get; private set; }
        [field: Space]
        [field: SerializeField] public int InventorySlots { get; private set; }
        [field: Space]
        [field: SerializeField] public float DefaultSanity { get; private set; }
        [field: SerializeField] public float DefaultSanityDrain { get; private set; }
        [field: Space]
        [field: SerializeField] public float BobSpeed { get; private set; }
        [field: SerializeField] public float BobAmount { get; private set; }
        [field: Space]
        [field: SerializeField] public float MinDropForce { get; private set; }
        [field: SerializeField] public float MaxDropForce { get; private set; }
        [field: Space]
        [field: SerializeField] public float RayCastDistance { get; private set; }
    }
}