using System.Threading.Tasks;
using DragonTrainer.Backend.Services.Http;
using NUnit.Framework;
using Moq;
using DragonTrainer.Backend.Services;
using System.Linq;
using System.Net.Http;

namespace DragonTrainer.Backend.Test.Services
{
    [TestFixture]
    public class ShopServiceTests
    {
        private Mock<IGameRequestor> _requestor;
        private ShopService _shopService;

        [SetUp]
        public void SetUp()
        {
            _requestor = new Mock<IGameRequestor>();
            _shopService = new ShopService(_requestor.Object);
        }

        [Test]
        public async Task GetItemList_RequestSuccessful_ReturnItems()
        {
            var json = "[ { \"Id\": \"abc\" },  { \"Id\": \"dong\" } ]";
            _requestor.Setup(g => g.GetRequest(It.IsAny<string>()))
                          .Returns(Task.FromResult(json));

            var result = await _shopService.GetItemList(It.IsAny<string>());

            Assert.That(result.FirstOrDefault().Id, Is.EqualTo("abc"));
        }

        [Test]
        public void GetItemList_IncorrectUri_ThrowHttpRequestException()
        {
            _requestor.Setup(g => g.GetRequest(It.IsAny<string>()))
                        .Throws(new HttpRequestException());

            Assert.That(
                () => _shopService.GetItemList(It.IsAny<string>()),
                Throws.Exception.TypeOf<HttpRequestException>()
            );
        }

        [Test]
        public async Task Purchase_RequestSuccessful_ReturnPurchaseResult()
        {
            var json = "{ \"ShoppingSuccess\": \"true\" }";
            _requestor.Setup(g => g.PostRequest(It.IsAny<string>(), null))
                          .Returns(Task.FromResult(json));

            var result = await _shopService.Purchase(It.IsAny<string>(), It.IsAny<string>());

            Assert.That(
                result.ShoppingSuccess, Is.EqualTo(true));
        }

        [Test]
        public void Purchase_IncorrectUri_ThrowhttpRequestException()
        {
            _requestor.Setup(g => g.PostRequest(It.IsAny<string>(), null))
                          .Throws(new HttpRequestException());

            Assert.That(
                () => _shopService.Purchase(It.IsAny<string>(), It.IsAny<string>()),
                Throws.Exception.TypeOf<HttpRequestException>()
            );
        }

    }
}