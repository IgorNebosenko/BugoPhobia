using System;
using System.Linq;
using ElectrumGames.GlobalEnums;
using UnityEngine;

namespace ElectrumGames.Configs
{
    [Serializable]
    public class MissionsNamesPair
    {
        [field: SerializeField] public MissionType MissionType { get; private set; }
        [field: SerializeField] public string MissionName { get; private set; }
    }

    [CreateAssetMenu(fileName = "Missions Names", menuName = "Configs/MissionsNames")]
    public class MissionsNames : ScriptableObject
    {
        [field: SerializeField] public MissionsNamesPair[] MissionsNamesPairs { get; private set;  }

        public string GetNameByType(MissionType missionType)
        {
            return MissionsNamesPairs.First(x => x.MissionType == missionType).MissionName;
        }
    }
}