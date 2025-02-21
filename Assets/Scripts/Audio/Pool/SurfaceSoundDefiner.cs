using ElectrumGames.Audio.Steps;
using UnityEngine;

namespace ElectrumGames.Audio.Pool
{
    public class SurfaceSoundDefiner : MonoBehaviour
    {
        [field: SerializeField] public SurfaceType SurfaceType { get; private set; }
    }
}