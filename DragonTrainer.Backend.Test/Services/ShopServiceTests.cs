using System.Threading.Tasks;
using DragonTrainer.Backend.Services.Http;
using Newtonsoft.Json;
using NUnit.Framework;
using DragonTrainer.Backend.DTOs;
using System.Net.Http;
using DragonTrainer.Backend.Services;
using System;

namespace DragonTrainer.Backend.Test.Services
{
    [TestFixture]
    public class ShopServiceTests
    {
       [SetUp] 
       public void SetUp()
       {

       }

       [Test]
       public async Task Purchase_Scenario_Return()
       {
            var gameRequestor = new GameRequestor(new HttpClient());
            var newGameResult = await gameRequestor.PostRequest(
                "https://dragonsofmugloar.com/api/v2/game/start",
                null
                );
            var gameInfo = JsonConvert.DeserializeObject<GameInfo>(newGameResult);

            var shopService = new ShopService(gameRequestor);
            var itemList = await shopService.GetItemList(gameInfo.GameId);
            var result = await shopService.Purchase(itemList[0].Id, gameInfo.GameId);

            Console.WriteLine(
                $" Success: {result.ShoppingSuccess}, Gold: {result.Gold}, Level: {result.Level}, Turn: {result.Turn}, Lives: {result.Lives}");
       }
    }
}