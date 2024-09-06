using System;
using UnityEngine;

namespace ElectrumGames.Configs
{
    [Serializable]
    public class FpsData
    {
        [field: SerializeField] public string fpsName;
        [field: SerializeField] public int fpsValue;
    }

    [CreateAssetMenu(fileName = "Fps Config", menuName = "Settings/FpsConfig")]
    public class FpsConfig : ScriptableObject
    {
        [field: SerializeField] public int DefaultIndex { get; private set; } = 1;
        [field: SerializeField] public FpsData[] Data { get; private set; }
    }
}