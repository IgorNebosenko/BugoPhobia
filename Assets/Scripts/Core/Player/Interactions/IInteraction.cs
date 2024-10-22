namespace ElectrumGames.Core.Player.Interactions
{
    public interface IInteraction
    {
        bool PutInteraction { get; }
        bool PrimaryInteraction { get; }
        bool AlternativeInteraction { get; }
        bool ExternalInteraction { get; }
        bool VoiceActivation { get; }
        bool FlashLightInteraction { get; }
        
        bool FirstSlotSelected { get; }
        bool SecondSlotSelected { get; }
        bool ThirdSlotSelected { get; }
        bool FourthSlotSelected { get; }
        bool NextSlotSelected { get; }
        
        bool DropItem { get; }

        void Init();
    }
}