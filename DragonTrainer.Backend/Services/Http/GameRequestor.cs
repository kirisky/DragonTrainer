using System.Net.Http;
using System.Threading.Tasks;
using DragonTrainer.Backend.Helpers;

namespace DragonTrainer.Backend.Services.Http
{
    public class GameRequestor : IGameRequestor
    {
        private HttpClient _httpClient;

        public GameRequestor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetRequest(string uri)
        {
            InfoHelper.DisplayGetRequestInfo(uri);

            return await _httpClient.GetStringAsync(uri);
        }

        public async Task<string> PostRequest(string uri, HttpContent content)
        {
            InfoHelper.DisplayPostRequestInfo(uri);

            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}