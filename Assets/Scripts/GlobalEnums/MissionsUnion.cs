namespace ElectrumGames.GlobalEnums
{
    public readonly struct MissionsUnion
    {
        public readonly MissionType FirstMissionType;
        public readonly bool FirstMissionStatus;
        
        public readonly MissionType SecondMissionType;
        public readonly bool SecondMissionStatus;
        
        public readonly MissionType ThirdMissionType;
        public readonly bool ThirdMissionStatus;

        public static MissionsUnion Empty()
        {
            return new MissionsUnion(MissionType.None, false, 
                MissionType.None, false, MissionType.None, false);
        }
        
        public MissionsUnion(MissionType firstMissionType, bool firstMissionStatus, MissionType secondMissionType,
            bool secondMissionStatus, MissionType thirdMissionType, bool thirdMissionStatus)
        {
            FirstMissionType = firstMissionType;
            FirstMissionStatus = firstMissionStatus;
            SecondMissionType = secondMissionType;
            SecondMissionStatus = secondMissionStatus;
            ThirdMissionType = thirdMissionType;
            ThirdMissionStatus = thirdMissionStatus;
        }
    }
}