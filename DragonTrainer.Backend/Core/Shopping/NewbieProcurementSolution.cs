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
            if (!PurchaseItem("Healing potion"))
            {
                // write some logs here
                return false;
            }

            return true;
        }

        public bool PurchaseBuffs()
        {
            if (!PurchaseItem("Claw Sharpening"))
            {
                // write some logs here 
                return false;
            }

            return true;
        }

        private bool PurchaseItem(string itemName)
        {
            var item = _items.Where(i => i.Name.Equals(itemName)).FirstOrDefault();
            var result = ShopService.Purchase(UserInfo.GameId, item.Id).Result;
            
            if (result.ShoppingSuccess)
            //     UserInfo.Gold = result.Gold;
            //     UserInfo.Level = result.Level;
            //     UserInfo.Lives = result.Lives;
            //     UserInfo.Turn = result.Turn;
                UserInfo = Mapper.Map<PurchaseResult, UserInfo>(result);

            return result.ShoppingSuccess;            
        }
    }
}