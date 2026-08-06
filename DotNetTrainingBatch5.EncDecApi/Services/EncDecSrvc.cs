using System.Text;
using Effortless.Net.Encryption;

namespace DotNetTrainingBatch5.EncDecApi.Services
{
    public class EncDecSrvc
    {
        private readonly byte[] key;
        private readonly byte[] iv;
        public EncDecSrvc(IConfiguration configuration)
        {
            key = Encoding.ASCII.GetBytes(configuration["Security:keycode"]!);
            iv = Encoding.ASCII.GetBytes(configuration["Security:ivcode"]!);
        }

        public string encryptData(string plainText)
        {
            return Strings.Encrypt(plainText, key, iv);
        }

        public string decryptData(string plainText) 
        { 
            return Strings.Decrypt(plainText, key, iv);
        }
    }
}
