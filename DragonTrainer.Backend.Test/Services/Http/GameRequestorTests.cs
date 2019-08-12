using System.Net.Http;
using System.Threading.Tasks;
using DragonTrainer.Backend.Services.Http;
using DragonTrainer.Backend.DTOs;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using DragonTrainer.Backend.DTOs.Shop;
using System;
using DragonTrainer.Backend.DTOs.Missions;
using System.Linq;
using System.Text;

namespace DragonTrainer.Backend.Test.Services.Http
{
    [TestFixture]
    public class GameRequestorTests
    {
        private Mock<HttpClient> _httpClient;
        private Mock<HttpContent> _httpContent;

        [SetUp]
        public void SetUp()
        {
            _httpClient = new Mock<HttpClient>();
            _httpContent = new Mock<HttpContent>();
        }

        [Test]
        public void GetRequest_Scenario_Return()
        {

        }


        [Test]
        public void GetRequest_Scenario2_Return2()
        {

        }

        public async Task PostRequest_StartANewGame_ReturnGameInfoObject()
        {
            var gameRequestor = new GameRequestor(_httpClient.Object);
            var result = await gameRequestor.PostRequest(
                "",
                _httpContent.Object
            );
        }

        [Test]
        public async Task PostRequest_Scenario_Return()
        {
            var gameRequestor = new GameRequestor(new HttpClient());
            var newGameResult = await gameRequestor.PostRequest(
                "https://dragonsofmugloar.com/api/v2/game/start",
                null
                );
            var GameInfo = JsonConvert.DeserializeObject<GameInfo>(newGameResult);

            var missionListUri = "https://dragonsofmugloar.com/api/v2/:gameId/messages";
            missionListUri = missionListUri.Replace(":gameId", GameInfo.GameId);
            var missionListJson = await gameRequestor.GetRequest(missionListUri);
            var missionList = JsonConvert.DeserializeObject<List<MissionInfo>>(missionListJson);

            var mission = missionList.FirstOrDefault();

            var performTaskUri = "https://dragonsofmugloar.com/api/v2/:gameId/solve/:adId";
            performTaskUri = performTaskUri.Replace(":gameId", GameInfo.GameId).Replace(":adId", mission.AdId);
            Console.WriteLine(performTaskUri);
            var response = await gameRequestor.PostRequest(
                performTaskUri,
                // httpContent
                null
            );

            var missionResult = JsonConvert.DeserializeObject<MissionResult>(response);
            Console.WriteLine($"Result: {missionResult.Success}, Gold: {missionResult.Gold}");


        }
    }
}