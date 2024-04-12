using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace EnableBankingTest.Helper_Methods
{
    public class FileContentOperations
    {
        public static async Task<X509Certificate2>  GetCertificate(string url)
        {
            string fileContent = "";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    fileContent = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception($"Failed to download file. Status code: {response.StatusCode}");
                }
            }
            // Remove file content except in the portion of "BEGIN CERTIFICATE" and "END CERTIFICATE" markers
            string pemContent = Regex.Replace(fileContent, @"(?s)^.*?-----BEGIN CERTIFICATE-----|-----END CERTIFICATE-----.?", "");


            // Decode Base64 content
            byte[] certificateBytes = Convert.FromBase64String(pemContent);


            // Create X509Certificate2 object from byte array
            var certificate = new X509Certificate2(certificateBytes);
            return certificate;
        }
    }
}
