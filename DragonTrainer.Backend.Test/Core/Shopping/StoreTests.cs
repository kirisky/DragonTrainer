using System.Collections.Generic;
using System.Threading.Tasks;
using DragonTrainer.Backend.Core.Shopping;
using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.DTOs.Shop;
using DragonTrainer.Backend.Services;
using Moq;
using NUnit.Framework;

namespace DragonTrainer.Backend.Test.Core.Shopping
{
    [TestFixture]
    public class StoreTests
    {
        private Mock<IProcurementSolution> _solution;
        private Mock<IShopService> _shopService;
        private Store _store;

        [SetUp]
        public void SetUp()
        {
            _solution = new Mock<IProcurementSolution>();
            _shopService = new Mock<IShopService>();
            _store = new Store(_solution.Object, _shopService.Object, DataHelper.GetUserInfo());
        }

        [Test]
        public void RefreshStore_CalledGetItems()
        {
            _store.RefreshStore();
            _solution.Verify(s => s.GetItems(_shopService.Object, It.IsAny<string>()));
        }

        [Test]
        public void RecoverHP_PickAItem_ReturnTrue()
        {
            var item = new ItemInfo
            {
                Id = "cs"
            };
            _solution.Setup(s => s.PickAPotion(It.IsAny<List<ItemInfo>>(),
                                               It.IsAny<int>()))
                                  .Returns(item);

            var purchaseResult = new PurchaseResult
            {
                ShoppingSuccess = true
            };
            _shopService.Setup(s => s.Purchase(It.IsAny<string>(), It.IsAny<string>()))
                        .Returns(Task.FromResult(purchaseResult));

            var result = _store.RecoverHP();

            _solution.Verify(s => s.PickAPotion(It.IsAny<List<ItemInfo>>(),
                                                It.IsAny<int>()));
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void RecoverHP_PickNothing_ReturnFalse()
        {
            _solution.Setup(s => s.PickAPotion(It.IsAny<List<ItemInfo>>(),
                                               It.IsAny<int>()))
                                  .Returns(value: null);

            var result = _store.RecoverHP();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void RecoverHP_PurchaseSuccessful_ReturnTrue()
        {
            var item = new ItemInfo
            {
                Id = "hpot"
            };
            _solution.Setup(s => s.PickAPotion(It.IsAny<List<ItemInfo>>(),
                                               It.IsAny<int>()))
                                  .Returns(item);


            var purchaseResult = new PurchaseResult
            {
                ShoppingSuccess = true
            };
            _shopService.Setup(s => s.Purchase(It.IsAny<string>(), It.IsAny<string>()))
                        .Returns(Task.FromResult(purchaseResult));

            var result = _store.RecoverHP();

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void RecoverHP_PurchaseFailed_ReturnFalse()
        {
            var item = new ItemInfo
            {
                Id = "hpot"
            };
            _solution.Setup(s => s.PickAPotion(It.IsAny<List<ItemInfo>>(),
                                               It.IsAny<int>()))
                                  .Returns(item);


            var purchaseResult = new PurchaseResult
            {
                ShoppingSuccess = false
            };
            _shopService.Setup(s => s.Purchase(It.IsAny<string>(), It.IsAny<string>()))
                        .Returns(Task.FromResult(purchaseResult));

            var result = _store.RecoverHP();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void LevelUp_PickAnItem_ReturnTrue()
        {
            var item = new ItemInfo
            {
                Id = "cs"
            };
            _solution.Setup(s => s.PickALevelUpItem(It.IsAny<List<ItemInfo>>(),
                                                    It.IsAny<int>()))
                                  .Returns(item);

            var purchaseResult = new PurchaseResult
            {
                ShoppingSuccess = true
            };
            _shopService.Setup(s => s.Purchase(It.IsAny<string>(), It.IsAny<string>()))
                        .Returns(Task.FromResult(purchaseResult));

            var result = _store.LevelUp();

            _solution.Verify(s => s.PickALevelUpItem(It.IsAny<List<ItemInfo>>(),
                                                It.IsAny<int>()));
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void LevelUp_PickNothing_ReturnFalse()
        {
            _solution.Setup(s => s.PickALevelUpItem(It.IsAny<List<ItemInfo>>(),
                                                    It.IsAny<int>()))
                                  .Returns(value: null);

            var result = _store.LevelUp();

            Assert.That(result, Is.EqualTo(false));
        }

       [Test]
        public void LevelUp_PurchaseSuccessful_ReturnTrue()
        {
            var item = new ItemInfo
            {
                Id = "cs"
            };
            _solution.Setup(s => s.PickALevelUpItem(It.IsAny<List<ItemInfo>>(),
                                                    It.IsAny<int>()))
                                  .Returns(item);


            var purchaseResult = new PurchaseResult
            {
                ShoppingSuccess = true
            };
            _shopService.Setup(s => s.Purchase(It.IsAny<string>(), It.IsAny<string>()))
                        .Returns(Task.FromResult(purchaseResult));

            var result = _store.LevelUp();

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void LevelUp_PurchaseFailed_ReturnFalse()
        {
            var item = new ItemInfo
            {
                Id = "cs"
            };
            _solution.Setup(s => s.PickALevelUpItem(It.IsAny<List<ItemInfo>>(),
                                                    It.IsAny<int>()))
                                  .Returns(item);


            var purchaseResult = new PurchaseResult
            {
                ShoppingSuccess = false
            };
            _shopService.Setup(s => s.Purchase(It.IsAny<string>(), It.IsAny<string>()))
                        .Returns(Task.FromResult(purchaseResult));

            var result = _store.LevelUp();

            Assert.That(result, Is.EqualTo(false));
        }
    }
}