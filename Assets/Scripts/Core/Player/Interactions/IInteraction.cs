namespace Core.Player.Interactions
{
    public interface IInteraction
    {
        bool ExternalInteraction { get; }
        bool PrimaryInteraction { get; }
        bool AlternativeInteraction { get; }
        bool VoiceActivation { get; }

        void Init();
    }
}