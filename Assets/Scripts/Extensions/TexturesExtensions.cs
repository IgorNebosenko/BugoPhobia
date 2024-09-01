using System.IO;
using UnityEngine;

namespace ElectrumGames.Extensions
{
    public static class TexturesExtensions
    {
        public static Texture2D FillTexture(this Texture2D texture, Color color)
        {
            var texture2D = new Texture2D(texture.width, texture.height);
            texture2D.name = "ClearTexture";
            var colors = new Color[texture2D.width * texture2D.height];
            for (var i = 0; i < colors.Length; i++)
                colors[i] = color;
            

            texture2D.SetPixels(colors);
            return texture2D;
        }

        public static Texture2D TextureFromPath(this string path)
        {
            Texture2D tmp = null;
            byte[] fileData;
            if (!File.Exists(path))
                return null;
            

            using (var fs = new FileStream(path, FileMode.Open))
            {
                fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int) fs.Length);
                fs.Flush();
                fs.Dispose();
                fs.Close();
            }
            
            tmp = new Texture2D(2, 2);
            tmp.LoadImage(fileData);
            return tmp;
        }

        public static void TextureFromPath(this string path, ref Texture2D texture)
        {
            byte[] fileData;
            if (!File.Exists(path))
                return;
            

            using (var fs = new FileStream(path, FileMode.Open))
            {
                fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int) fs.Length);
                fs.Flush();
                fs.Dispose();
                fs.Close();
            }


            try
            {
                texture.LoadImage(fileData);
            }
            catch
            {
                texture = null;
            }
        }

        public static Sprite Texture2DToSprite(this Texture2D texture2D)
        {
            if (texture2D == null)
                return null;
            return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.one * 0.5f);
        }

        public static Sprite Texture2DToSprite(this Texture2D texture2D, Vector2 pivot)
        {
            return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), pivot);
        }
    }
}