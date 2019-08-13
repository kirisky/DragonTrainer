using System.Net.Http;
using System.Threading.Tasks;
using DragonTrainer.Backend.Services;
using DragonTrainer.Backend.Services.Http;
using Moq;
using NUnit.Framework;

namespace DragonTrainer.Backend.Test.Services
{
    [TestFixture]
    public class GameServiceTests
    {
        private Mock<IGameRequestor> _gameRequestor;
        private IGameService _gameService;

        [SetUp]
        public void SetUp()
        {
            _gameRequestor = new Mock<IGameRequestor>();
            _gameService = new GameService(_gameRequestor.Object);
        }

        [Test]
        public async Task StartNewGame_StartANewGame_ReturnGameInfo()
        {
            var json = "{ \"GameId\": \"abc\" }";
            _gameRequestor.Setup(g => g.PostRequest(It.IsAny<string>(), null))
                          .Returns(Task.FromResult(json));

            var result = await _gameService.StartNewGame();

            Assert.That(result.GameId, Is.EqualTo("abc"));
        }

        [Test]
        public void StartNewGame_IncorrectUri_ThrowHttpRequestException()
        {
            _gameRequestor.Setup(g => g.PostRequest(It.IsAny<string>(), null))
                            .Throws(new HttpRequestException());

            Assert.That(
                () => _gameService.StartNewGame(),
                Throws.Exception.TypeOf<HttpRequestException>()
            );
        }
    }
}