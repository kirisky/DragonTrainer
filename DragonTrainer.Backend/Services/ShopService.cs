using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DragonTrainer.Backend.DTOs.Shop;
using DragonTrainer.Backend.Services.Http;
using Newtonsoft.Json;

namespace DragonTrainer.Backend.Services
{
    public class ShopService
    {
        private readonly GameRequestor _requestor;
        private readonly string _baseUri;
        private readonly string _itemsUri;
        private readonly string _itemPurchasingUri;

        public ShopService(GameRequestor gameRequestor)
        {
            _requestor = gameRequestor;
            _baseUri = "https://dragonsofmugloar.com";
            _itemsUri = "/api/v2/:gameId/shop";
            _itemPurchasingUri = "/api/v2/:gameId/shop/buy/:itemId";
        }

        public async Task<List<ItemInfo>> GetItemList(string gameId)
        {
            var content = await _requestor.GetRequest(
                _baseUri + _itemsUri.Replace(":gameId", gameId)
            );
            return JsonConvert.DeserializeObject<List<ItemInfo>>(content);
        }

        public async Task<PurchaseResult> Purchase(string gameId, string itemId)
        {
            var uri = _baseUri +
                      _itemPurchasingUri
                        .Replace(":gameId", gameId)
                        .Replace(":itemId", itemId);

            var content = await _requestor.PostRequest(uri, null);
            
            return JsonConvert.DeserializeObject<PurchaseResult>(content);
        }


    }
}