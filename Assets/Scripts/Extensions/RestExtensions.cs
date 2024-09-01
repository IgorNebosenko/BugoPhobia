namespace ElectrumGames.Extensions
{
    public static class RestExtensions
    {
        public static string MakePageableUrl(this string url, long count, long page = 0)
        {
            return $"{url}?page={page}&size={count}";
        }
    }
}