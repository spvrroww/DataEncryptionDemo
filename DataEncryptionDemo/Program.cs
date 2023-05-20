// See https://aka.ms/new-console-template for more information
using DataEncryptionDemo;

Console.WriteLine("Hello, World!");

StringDataEncryptionWithInputPassphrase encryption = new();

string message = "This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works. This is a test to see how string data encrytpion works.";

const string passphrase = "ExcellentPassword357siPS";

var encryptedResult = await encryption.EncryptData(message, passphrase);
Console.WriteLine($"EncryptedData: {BitConverter.ToString( encryptedResult.Data)}");

var decryptedResult = await encryption.DecryptData(encryptedResult);
Console.WriteLine($"DecryptedData: {decryptedResult}");
Console.ReadKey();