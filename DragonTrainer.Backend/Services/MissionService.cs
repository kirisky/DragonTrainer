using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DragonTrainer.Backend.DTOs.Missions;
using DragonTrainer.Backend.Services.Http;
using Newtonsoft.Json;

namespace DragonTrainer.Backend.Services
{
    public class MissionService
    {
        private readonly GameRequestor _requestor;
        private readonly string _baseUri;
        private readonly string _missionsUri;
        private readonly string _missionSolvingUri;

        public MissionService(GameRequestor gameRequestor)
        {
            _requestor = gameRequestor;
            _baseUri = "https://dragonsofmugloar.com";
            _missionsUri = "/api/v2/:gameId/messages";
            _missionSolvingUri = "/api/v2/:gameId/solve/:adId";
        }

        public async Task<List<MissionInfo>> GetMissionList(string gameId)
        {
            var content = await _requestor.GetRequest(
                _baseUri + _missionsUri.Replace(":gameId", gameId)
            );
            return JsonConvert.DeserializeObject<List<MissionInfo>>(content);
        }

        public async Task<MissionResult> SovleMission(string gameId, string missionId)
        {
            var uri = _baseUri + 
                      _missionSolvingUri
                        .Replace(":gameId", gameId)
                        .Replace(":adId", missionId);

            var content = await _requestor.PostRequest(uri, null);
            return JsonConvert.DeserializeObject<MissionResult>(content);
        }
    }
}