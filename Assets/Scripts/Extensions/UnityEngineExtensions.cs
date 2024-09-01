namespace ElectrumGames.Extensions
{
    public static class UnityEngineExtensions
    {
        public static bool UnityNullCheck(this System.Object obj)
        {
            return obj == null || obj.Equals(null);
        }
    }
}