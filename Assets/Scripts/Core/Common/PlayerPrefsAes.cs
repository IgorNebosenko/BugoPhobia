using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace ElectrumGames.Core.Common
{
    public static class PlayerPrefsAes
    {
        private const string AESKey = "de4rngnw74ver34gk43kg5ksnve95f";

        public static void SetEncryptedString(string encryptedString, string key)
        {
            PlayerPrefs.SetString(key, EncryptAES(encryptedString));
        }

        public static string GetEncryptedString(string key, string defaultValue = "")
        {
            var encryptedString = PlayerPrefs.GetString(key, string.Empty);
            if (string.IsNullOrEmpty(encryptedString)) 
                return defaultValue;

            return DecryptAES(encryptedString);
        }

        public static void SetEncryptedInt(string key, int value)
        {
            var intString = value.ToString(CultureInfo.InvariantCulture);
            var encryptedInt = EncryptAES(intString);
            PlayerPrefs.SetString(key, encryptedInt);
        }

        public static int GetEncryptedInt(string key, int defaultValue = 0)
        {
            var encryptedString = PlayerPrefs.GetString(key, string.Empty);
            if (string.IsNullOrEmpty(encryptedString)) 
                return defaultValue;

            var decryptedString = DecryptAES(encryptedString);
            return int.TryParse(decryptedString, out var result) ? result : defaultValue;
        }
        
        public static void SetEncryptedDecimal(string key, decimal value)
        {
            var decimalString = value.ToString(CultureInfo.InvariantCulture);
            var encryptedString = EncryptAES(decimalString);
            PlayerPrefs.SetString(key, encryptedString);
        }

        public static decimal GetEncryptedDecimal(string key, decimal defaultValue = 0m)
        {
            var encryptedString = PlayerPrefs.GetString(key, string.Empty);
            if (string.IsNullOrEmpty(encryptedString)) 
                return defaultValue;

            var decryptedString = DecryptAES(encryptedString);
            return decimal.TryParse(decryptedString, out var result) ? result : defaultValue;
        }
        
        private static string EncryptAES(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = GetKeyBytes();
            aes.IV = new byte[16];

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 
                0, plainBytes.Length);

            return Convert.ToBase64String(encryptedBytes);
        }

        private static string DecryptAES(string cipherText)
        {
            byte[] plainBytes = {};
            try
            {
                using var aes = Aes.Create();
                aes.Key = GetKeyBytes();
                aes.IV = new byte[16];

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                var cipherBytes = Convert.FromBase64String(cipherText);
                plainBytes = decryptor.TransformFinalBlock(cipherBytes,
                    0, cipherBytes.Length);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            return Encoding.UTF8.GetString(plainBytes);
            
        }

        private static byte[] GetKeyBytes()
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(AESKey));
        }
    }
}
