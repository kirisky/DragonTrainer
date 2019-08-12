using System.Net.Http;
using System.Threading.Tasks;
using DragonTrainer.Backend.DTOs.Investigation;
using DragonTrainer.Backend.Services.Http;
using Newtonsoft.Json;

namespace DragonTrainer.Backend.Services
{
    public class InvestigationService
    {
        private readonly GameRequestor _requestor;
        private readonly string _baseUri;
        private readonly string _investigationUri;

        public InvestigationService(GameRequestor gameRequestor)
        {
            _requestor = gameRequestor;
            _baseUri = "https://dragonsofmugloar.com";
            _investigationUri = "/api/v2/:gameId/investigate/reputation";
        }

        public async Task<Reputation> Investigate(string gameId)
        {
            var uri = _baseUri + _investigationUri.Replace(":gameId", gameId);

            var content = await _requestor.PostRequest(uri, null);
            return JsonConvert.DeserializeObject<Reputation>(content);
        }
    }
}