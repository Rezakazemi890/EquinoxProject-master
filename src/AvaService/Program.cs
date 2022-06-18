using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Sample
{
    public class Program
    {
        static async Task Main(string[] args)
        {
        }

        public static string Encrypt(string data)
        {
            var eccPem = File.ReadAllText("public_key.pem");
            var key = RSA.Create();
            key.ImportSubjectPublicKeyInfo(Convert.FromBase64String(eccPem.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "")), out _);
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            byte[] bytesEncrypted = key.Encrypt(byteData, RSAEncryptionPadding.Pkcs1);
            var output = Convert.ToBase64String(bytesEncrypted);
            return output;
        }
    }
}
