namespace ElectrumGames.Core.Common
{
    public interface ISwitchInteractable : IGhostInteractable
    {
        void SwitchOn();
        void SwitchOff();

        void TrySwitchOffByGhost();
    }
}