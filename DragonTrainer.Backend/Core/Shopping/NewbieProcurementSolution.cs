using System.Collections.Generic;
using System.Linq;
using DragonTrainer.Backend.DTOs.Shop;
using DragonTrainer.Backend.Services;

namespace DragonTrainer.Backend.Core.Shopping
{
    public class NewbieProcurementSolution : IProcurementSolution
    {
        public List<ItemInfo> GetItems(IShopService shopService, string gameId)
        {
            return shopService.GetItemList(gameId).Result;
        }

        public ItemInfo PickAPotion(List<ItemInfo> items, int userBalance)
        {
            return FetchItem(items, "Healing potion");
        }

        public ItemInfo PickALevelUpItem(List<ItemInfo> items, int userBalance)
        {
            if (userBalance > 400)
            {
                return FetchItem(items, "Potion of Awesome Wings");
            }

            if (userBalance > 200)
            {
                return  FetchItem(items, "Claw Sharpening");
            }

            return null;
        }

        private ItemInfo FetchItem(List<ItemInfo> items, string itemName)
        {
            return items.Where(i => i.Name.Equals(itemName)).FirstOrDefault();
        }
    }
}