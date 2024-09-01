namespace ElectrumGames.Core.Player.Movement
{
    public interface IMotor
    {
        void Simulate(IInput input, float deltaTime);
        void FixedSimulate(IInput input, float deltaTime);
    }
}