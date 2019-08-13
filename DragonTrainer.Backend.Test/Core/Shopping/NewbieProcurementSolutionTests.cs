using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DragonTrainer.Backend.Core.Shopping;
using DragonTrainer.Backend.DTOs.Shop;
using DragonTrainer.Backend.Services;
using Moq;
using NUnit.Framework;

namespace DragonTrainer.Backend.Test.Core.Shopping
{
    [TestFixture]
    public class NewbieProcurementSolutionTests
    {
        private Mock<IShopService> _shopService;
        private NewbieProcurementSolution _solution;
        private List<ItemInfo> _items;

        [SetUp]
        public void SetUp()
        {
            _shopService = new Mock<IShopService>();
            _solution = new NewbieProcurementSolution();
            _items = new List<ItemInfo>();
        }

        [Test] 
        public void GetItems_RequestSuccessful_ReturnItems()
        {
            _items.Add(new ItemInfo());

            _shopService.Setup(s => s.GetItemList(It.IsAny<string>()))
                        .Returns(Task.FromResult(_items));

            var result = _solution.GetItems(_shopService.Object, It.IsAny<string>());

            Assert.That(result.Any(), Is.EqualTo(true));
        }

        [Test] 
        public void GetItems_RequestFailed_ReturnEmptyContainer()
        {
            _shopService.Setup(s => s.GetItemList(It.IsAny<string>()))
                        .Returns(Task.FromResult(_items));

            var result = _solution.GetItems(_shopService.Object, It.IsAny<string>());

            Assert.That(result.Any(), Is.EqualTo(false));
        }

        public void GetItems_IncorrectArguments_ThrowHttpRequestException()
        {
           _shopService.Setup(s => s.GetItemList(It.IsAny<string>()))
                        .Throws(new HttpRequestException());

            var result = _solution.GetItems(_shopService.Object, It.IsAny<string>());

            Assert.That(
                () => _solution.GetItems(_shopService.Object, It.IsAny<string>()),
                Throws.Exception.TypeOf<HttpRequestException>()
            );
        }

        [Test] 
        public void PickALevelUpItem_GoldMoreThenFourHundred_ReturnWingpotmax()
        {
            var itemId = "wingpotmax";
            _items.Add(new ItemInfo {
                Id = itemId,
                Name = "Potion of Awesome Wings",
            });

            var result = _solution.PickALevelUpItem(_items,  1000);

            Assert.That(result.Id, Is.EqualTo(itemId));
        }

        [Test] 
        public void PickALevelUpItem_GoldMoreThenTwoHundred_ReturnCS()
        {
            var itemId = "cs";
            _items.Add(new ItemInfo {
                Id = itemId,
                Name = "Claw Sharpening"
            });

            var result = _solution.PickALevelUpItem(_items, 300);

            Assert.That(result.Id, Is.EqualTo(itemId));
        }

        [Test] 
        public void PickALevelUpItem_WithoutEnoughMoneyToPurchase_ReturnNull()
        {
            var result = _solution.PickALevelUpItem(_items,  30);

            Assert.That(result, Is.Null);
        }
    }
}