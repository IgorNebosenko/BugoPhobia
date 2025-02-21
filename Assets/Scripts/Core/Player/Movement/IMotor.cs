namespace ElectrumGames.Core.Player.Movement
{
    public interface IMotor
    {
        bool CanSprint { get; }
        
        void Simulate(IInput input, float deltaTime);
        void FixedSimulate(IInput input, float deltaTime);
    }
}