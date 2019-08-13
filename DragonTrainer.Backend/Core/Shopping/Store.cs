using System.Collections.Generic;
using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.DTOs.Shop;
using DragonTrainer.Backend.Services;

namespace DragonTrainer.Backend.Core.Shopping
{
    public class Store
    {
        private IProcurementSolution _solution;
        private IShopService _shopService;
        private UserInfo _userInfo;
        private List<ItemInfo> _items;

        public Store(IProcurementSolution solution, IShopService shopService, UserInfo userInfo)
        {
            _solution = solution;
            _shopService = shopService;
            _userInfo = userInfo;
        }

        public void RefreshStore()
        {
            _items = _solution.GetItems(_shopService, _userInfo.GameId);
        }

        public bool RecoverHP()
        {
            var item = _solution.PickAPotion(_items, _userInfo.Gold);
            if (item == null) return false;

            return PurchaseItem(item.Id);
        }

        public bool LevelUp()
        {
            var item = _solution.PickALevelUpItem(_items, _userInfo.Gold);
            if (item == null) return false;

            return PurchaseItem(item.Id);
        }

        private bool PurchaseItem(string itemId)
        {
            var result = _shopService.Purchase(_userInfo.GameId, itemId).Result;
            if (result.ShoppingSuccess)
            {
                _userInfo.Gold = result.Gold;
                _userInfo.Level = result.Level;
                _userInfo.Lives = result.Lives;
                _userInfo.Turn = result.Turn;

                return true;
            }

            return false;
        }
    }
}