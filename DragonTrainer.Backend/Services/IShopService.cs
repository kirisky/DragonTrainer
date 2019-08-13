using System.Collections.Generic;
using System.Threading.Tasks;
using DragonTrainer.Backend.DTOs.Shop;

namespace DragonTrainer.Backend.Services
{
    public interface IShopService
    {
        Task<List<ItemInfo>> GetItemList(string gameId);
        Task<PurchaseResult> Purchase(string gameId, string itemId);
    }
}