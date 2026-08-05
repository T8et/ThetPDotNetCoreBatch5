// See https://aka.ms/new-console-template for more information
using Effortless.Net.Encryption;
using System.Text;

Console.WriteLine("Hello, World!");
byte[] key = Encoding.UTF8.GetBytes("thisisasecretkey");
byte[] iv = Encoding.UTF8.GetBytes("thisisasecretkey");

string encrypted = Strings.Encrypt("kempoSecret", key, iv);
string decrypted = Strings.Decrypt(encrypted, key, iv);

Console.WriteLine("encrypted >> "+encrypted);
Console.WriteLine("decrypted >> "+decrypted);
Console.Read();