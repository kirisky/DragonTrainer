using System.Net.Http;
using System.Threading.Tasks;

namespace DragonTrainer.Backend.Services.Http
{
    public class GameRequestor
    {
        private HttpClient _httpClient;

        public GameRequestor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetRequest(string uri)
        {
            return await _httpClient.GetStringAsync(uri);
        }

        public async Task<string> PostRequest(string uri, HttpContent content)
        {
            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}