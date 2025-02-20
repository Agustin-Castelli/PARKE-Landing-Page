using System.Text;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PARKE_Landing_Page.Services
{
    public class GoogleTranslationService
    {
        private static readonly string apiKey = "AIzaSyDTmAEALL-4Do2m477GeJA2SU8I2YVYDKg"; // Reemplaza con tu clave de API
        private static readonly string endpoint = "https://translation.googleapis.com/language/translate/v2";

        public async Task<string> TranslateTextAsync(string text, string targetLanguage)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new
                {
                    q = text,
                    target = targetLanguage,
                    format = "text"
                };

                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{endpoint}?key={apiKey}", content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var translatedText = JObject.Parse(responseBody)["data"]["translations"][0]["translatedText"].ToString();

                return translatedText;
            }
        }
    }
}
