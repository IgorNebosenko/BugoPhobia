namespace ElectrumGames.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            if (str == null) 
                return true;
            return str.Length == 0;
        }

        public static string ToUpperFirstLetter(this string str)
        {
            var text = str.Substring(1);
            text = text.Insert(0, str[0].ToString().ToUpper());
            return text;
        }
    }
}