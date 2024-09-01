using UnityEngine;

namespace ElectrumGames.Extensions.CommonInterfaces
{
    public interface IHaveRotation
    {
        Quaternion Rotation { get; }
    }
}