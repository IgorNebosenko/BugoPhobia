namespace ElectrumGames.Core.Environment
{
    public class SwitchableLampEnvironmentObject : LightEnvironmentObject
    {
        private bool _lastState;
        
        public override void OnInteract()
        {
            _lastState = !_lastState;
            
            SwitchStateTo(_lastState);
        }
    }
}