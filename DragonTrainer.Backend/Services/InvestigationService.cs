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
        private readonly string _investigation;

        public InvestigationService(GameRequestor gameRequestor)
        {
            _requestor = gameRequestor;
            _baseUri = "https://dragonsofmugloar.com";
            _investigation = "/api/v2/:gameId/investigate/reputation";
        }

        public async Task<Reputation> Investigate(string gameId)
        {
            var json = JsonConvert.SerializeObject(new {
                gameId = gameId
            });
            var httpContent = new StringContent(json);

            var content = await _requestor.PostRequest(
                _baseUri + _investigation,
                httpContent
            );
            return JsonConvert.DeserializeObject<Reputation>(content);
        }
    }
}