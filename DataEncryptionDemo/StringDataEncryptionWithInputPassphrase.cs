using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataEncryptionDemo
{
    public class StringDataEncryptionWithInputPassphrase
    {
        //Method to convert Passphrase to Key

        private SecurityKeyModel GenerateSecurityKey(string passphrase, byte[] salt )
        {
            using HMACSHA256 hMACSHA256 = salt.Count() > 0 ? new HMACSHA256(salt) : new HMACSHA256();

            //var key = hMACSHA256.ComputeHash();
            //return key;
            var securityKey = Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(passphrase), hMACSHA256.Key, 1000, HashAlgorithmName.SHA384, 16);
            return new SecurityKeyModel(securityKey, hMACSHA256.Key);
            
        }

        //Method to encrypt data
        public async Task<EncryptionResult> EncryptData(string data, string passphrase)
        {
            using Aes aes = Aes.Create();
            var securityKeyModel = GenerateSecurityKey(passphrase, Array.Empty<byte>());
            aes.Key =  securityKeyModel.password;

            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, aes.CreateEncryptor(),CryptoStreamMode.Write);

            await cryptoStream.WriteAsync(Encoding.Unicode.GetBytes(data));
            await cryptoStream.FlushFinalBlockAsync();

            return new EncryptionResult(memoryStream.ToArray(), passphrase, aes.IV, securityKeyModel.key);
        }

        //Method to Decrypt Data
        public async Task<string> DecryptData(EncryptionResult encryptionResult)
        {
            using Aes aes = Aes.Create();
            var securityKeyModel = GenerateSecurityKey(encryptionResult.Passphrase,encryptionResult.salt);
            aes.Key = securityKeyModel.password;
            aes.IV = encryptionResult.IV;

            using MemoryStream input = new MemoryStream(encryptionResult.Data);
            using CryptoStream cryptoStream = new(input, aes.CreateDecryptor(), CryptoStreamMode.Read);

            using MemoryStream output = new MemoryStream();
            await cryptoStream.CopyToAsync(output);

            return Encoding.Unicode.GetString(output.ToArray());               
        }
    }
}
