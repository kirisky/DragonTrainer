using System.Collections.Generic;
using DragonTrainer.Backend.DTOs.Shop;
using DragonTrainer.Backend.Services;

namespace DragonTrainer.Backend.Core.Shopping
{
    public interface IProcurementSolution
    {
         List<ItemInfo> GetItems(IShopService shopService, string gameId);
         ItemInfo PickAPotion(List<ItemInfo> items, int userBalance);
         ItemInfo PickALevelUpItem(List<ItemInfo> items, int userBalance);
    }
}