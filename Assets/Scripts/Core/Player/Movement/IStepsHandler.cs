namespace ElectrumGames.Core.Player.Movement
{
    public interface IStepsHandler
    {
        void FixedSimulate(IInput input, float time);
    }
}