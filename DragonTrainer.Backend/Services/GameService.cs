using System.Threading.Tasks;
using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.Services.Http;
using Newtonsoft.Json;

namespace DragonTrainer.Backend.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRequestor _requestor;
        private readonly string _baseUri;
        private readonly string _newGameUri;

        public GameService(IGameRequestor gameRequestor)
        {
            _requestor = gameRequestor;
            _baseUri = "https://dragonsofmugloar.com";
            _newGameUri = "/api/v2/game/start";
        }

        public async Task<GameInfo> StartNewGame()
        {
            var content = await _requestor.PostRequest(
                _baseUri + _newGameUri,
                null
                );
            return JsonConvert.DeserializeObject<GameInfo>(content);
        }
    }
}