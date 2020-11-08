using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Definitions
{
    // Append Class by Teeramed Tangjirawattana 2562
    public interface IUtilityCommon
    {
        // Base64
        string Base64Encode(string plainText);
        string Base64Decode(string base64EncodedData);
        T Base64Decode<T>(string base64EncodedData);

        // MD5
        string MD5Hash(string input);

        // Encrypt with key
        string MD5HashWithKey(string input, string encryptionkey);
        string EncryptData(string textData, string encryptionkey);
        string EncryptData(object textData, string encryptionkey);
        string DecryptData(string encryptedText, string encryptionkey);
        T DecryptData<T>(string encryptedText, string encryptionkey);

        // Encrypt with key: Recommend for data id
        string EncryptDataUrlEncoder(string textData, string encryptionkey);
        string EncryptDataUrlEncoder(object textData, string encryptionkey);
        string DecryptDataUrlEncoder(string encryptedText, string encryptionkey);
        T DecryptDataUrlEncoder<T>(string encryptedText, string encryptionkey);
    }

    public class UtilityCommon : IUtilityCommon
    {
        #region [Base64]
        public string Base64Encode(string plainText)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(plainText))
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                result = Convert.ToBase64String(plainTextBytes);
            }
            return result;
        }

        public string Base64Decode(string base64EncodedData)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(base64EncodedData))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                result = Encoding.UTF8.GetString(base64EncodedBytes);
            }
            return result;
        }

        public T Base64Decode<T>(string base64EncodedData)
        {
            try
            {
                string result = this.Base64Decode(base64EncodedData: base64EncodedData);
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
        #endregion [Base64]

        #region [MD5]
        public string MD5Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        #endregion [MD5]

        #region [Encrypt with key]
        public string MD5HashWithKey(string input, string encryptionkey)
        {
            string result = string.Empty;
            string md5HashResult = this.MD5Hash(input: input);
            if (!string.IsNullOrEmpty(md5HashResult))
            {
                result = this.EncryptData(textData: md5HashResult, encryptionkey: encryptionkey);
            }
            return result;
        }

        public string EncryptData(string textData, string encryptionkey)
        {
            RijndaelManaged objrij = new RijndaelManaged();
            //set the mode for operation of the algorithm
            objrij.Mode = CipherMode.CBC;
            //set the padding mode used in the algorithm.
            objrij.Padding = PaddingMode.PKCS7;
            //set the size, in bits, for the secret key.
            objrij.KeySize = 0x80;
            //set the block size in bits for the cryptographic operation.
            objrij.BlockSize = 0x80;
            //set the symmetric key that is used for encryption & decryption.
            byte[] passBytes = Encoding.UTF8.GetBytes(encryptionkey);
            //set the initialization vector (IV) for the symmetric algorithm
            byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            objrij.Key = EncryptionkeyBytes;
            objrij.IV = EncryptionkeyBytes;
            //Creates symmetric AES object with the current key and initialization vector IV.
            ICryptoTransform objtransform = objrij.CreateEncryptor();
            byte[] textDataByte = Encoding.UTF8.GetBytes(textData);
            //Final transform the test string.
            return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
        }

        public string EncryptData(object textData, string encryptionkey)
        {
            string result = string.Empty;
            if (textData != null && textData?.ToString().Trim() != string.Empty)
            {
                result = this.EncryptData(textData: textData.ToString(), encryptionkey: encryptionkey);
            }
            return result;
        }

        public string DecryptData(string encryptedText, string encryptionkey)
        {
            RijndaelManaged objrij = new RijndaelManaged();
            objrij.Mode = CipherMode.CBC;
            objrij.Padding = PaddingMode.PKCS7;
            objrij.KeySize = 0x80;
            objrij.BlockSize = 0x80;
            byte[] encryptedTextByte = Convert.FromBase64String(encryptedText);
            byte[] passBytes = Encoding.UTF8.GetBytes(encryptionkey);
            byte[] EncryptionkeyBytes = new byte[0x10];
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            objrij.Key = EncryptionkeyBytes;
            objrij.IV = EncryptionkeyBytes;
            byte[] TextByte = objrij.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
            return Encoding.UTF8.GetString(TextByte);  //it will return readable string
        }

        public T DecryptData<T>(string encryptedText, string encryptionkey)
        {
            try
            {
                string decryptedData = this.DecryptData(encryptedText: encryptedText, encryptionkey: encryptionkey);
                return (T)Convert.ChangeType(decryptedData, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
        #endregion [Encrypt with key]

        #region [Encrypt with key: Recommend for data id]
        public string EncryptDataUrlEncoder(string textData, string encryptionkey)
        {
            return Base64UrlEncoder.Encode(this.EncryptData(textData: textData, encryptionkey: encryptionkey));
        }

        public string EncryptDataUrlEncoder(object textData, string encryptionkey)
        {
            return Base64UrlEncoder.Encode(this.EncryptData(textData: textData, encryptionkey: encryptionkey));
        }

        public string DecryptDataUrlEncoder(string encryptedText, string encryptionkey)
        {
            return this.DecryptData(encryptedText: Base64UrlEncoder.Decode(encryptedText), encryptionkey: encryptionkey);
        }

        public T DecryptDataUrlEncoder<T>(string encryptedText, string encryptionkey)
        {
            try
            {
                string decryptedData = this.DecryptData(encryptedText: Base64UrlEncoder.Decode(encryptedText), encryptionkey: encryptionkey);
                if (Nullable.GetUnderlyingType(typeof(T)) != null)
                {
                    return (T)Convert.ChangeType(decryptedData, Nullable.GetUnderlyingType(typeof(T)));
                }
                else
                {
                    return (T)Convert.ChangeType(decryptedData, typeof(T));
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return default(T);
            }
        }
        #endregion [Encrypt with key: Recommend for data id]
    }
}
