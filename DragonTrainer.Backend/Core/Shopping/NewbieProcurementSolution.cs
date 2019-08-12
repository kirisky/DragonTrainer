using System;
using System.Collections.Generic;
using System.Linq;
using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.DTOs.Shop;
using DragonTrainer.Backend.Helpers;
using DragonTrainer.Backend.Services;

namespace DragonTrainer.Backend.Core.Shopping
{
    public class NewbieProcurementSolution : IProcurementSolution
    {
        private List<ItemInfo> _items;

        public ShopService ShopService { get; set; }
        public UserInfo UserInfo { get; set; }
        public MapperHelper Mapper { get; set; }

        public void GetItems()
        {
            _items = ShopService.GetItemList(UserInfo.GameId).Result;
        }

        public bool PurchasePotions()
        {
            var item = FetchItem("Healing potion");
            Console.WriteLine(
                $"Name: {item.Name}",
                $"Id: {item.Id}",
                $"Cost: {item.Cost}"
            );
            if (item.Cost > UserInfo.Gold) return false;

            if (!PurchaseItem(item.Id))
            {
                // write some logs here
                return false;
            }

            return true;
        }

        public bool PurchaseLevels()
        {
            if (UserInfo.Gold > 400)
            {
                var item = FetchItem("Potion of Awesome Wings");
                return PurchaseItem(item.Id);
            }

            if (UserInfo.Gold > 200)
            {
                var item = FetchItem("Claw Sharpening");
                return PurchaseItem(item.Id);
            }
               
            return false;
        }

        private ItemInfo FetchItem(string itemName)
        {
            return _items.Where(i => i.Name.Equals(itemName)).FirstOrDefault();
        }

        private bool PurchaseItem(string itemId)
        {
            var result = ShopService.Purchase(UserInfo.GameId, itemId).Result;
            
            if (result.ShoppingSuccess)
            {
                UserInfo.Gold = result.Gold;
                UserInfo.Level = result.Level;
                UserInfo.Lives = result.Lives;
                UserInfo.Turn = result.Turn;
            }
                // UserInfo = Mapper.Map<PurchaseResult, UserInfo>(result);

            return result.ShoppingSuccess;            
        }

    }
}