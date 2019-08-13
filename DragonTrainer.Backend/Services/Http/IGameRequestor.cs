using System.Net.Http;
using System.Threading.Tasks;

namespace DragonTrainer.Backend.Services.Http
{
    public interface IGameRequestor
    {
        Task<string> GetRequest(string uri);
        Task<string> PostRequest(string uri, HttpContent content);
    }
}