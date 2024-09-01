namespace ElectrumGames.Extensions
{
    public static class NumbersExtensions
    {
        public static float Map(this float currentValue, float from, float to, float from2, float to2)
        {
            if (currentValue <= from2)
                return from;
            if (currentValue >= to2)
                return to;
            return (to - from) * ((currentValue - from2) / (to2 - from2)) + from;
        }


        public static int Map(this int currentValue, int from, int to, int from2, int to2)
        {
            return from2 + (int) ((to2 - from2) * ((currentValue - from) / (float) (to - from)));
        }
    }
}