using System.Net.Http;
using System.Threading.Tasks;

namespace DragonTrainer.Backend.Services.Http
{
    public class GameRequestor
    {
        public async Task<string> GetRequest(string uri)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(uri);
            }
        }

        public async Task<string> PostRequest(string uri, HttpContent content)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(uri, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}