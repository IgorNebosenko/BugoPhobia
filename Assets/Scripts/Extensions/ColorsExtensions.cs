using UnityEngine;

namespace ElectrumGames.Extensions
{
    public static class ColorsExtensions
    {
        public static Color ColorFromString(this string colorText)
        {
            colorText = colorText.Trim();
            Color color;
            if (colorText.StartsWith("#"))
                ColorUtility.TryParseHtmlString(colorText, out color);
            else
                color = colorText.ConvertRgbaToHex();

            return color;
        }

        public static Color ConvertRgbaToHex(this string rgba)
        {
            var colorsContainer = rgba.Split('(', ')');
            var colors = colorsContainer[1].Split(',');
            var r = float.Parse(colors[0]) / 255f;
            var g = float.Parse(colors[1]) / 255f;
            var b = float.Parse(colors[2]) / 255f;
            var color = new Color(r, g, b);
            if (colors.Length > 3)
            {
                var a = float.Parse(colors[3]);
                color = new Color(r, g, b, a);
            }

            return color;
        }
    }
}