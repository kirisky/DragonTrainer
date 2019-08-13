using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DragonTrainer.Backend.Services;
using DragonTrainer.Backend.Services.Http;
using Moq;
using NUnit.Framework;

namespace DragonTrainer.Backend.Test.Services
{
    [TestFixture]
    public class MissionServiceTests
    {
        private Mock<IGameRequestor> _requestor;
        private MissionService _missionService;

        [SetUp]
        public void SetUp()
        {
            _requestor = new Mock<IGameRequestor>();
            _missionService = new MissionService(_requestor.Object);
        }

        [Test]
        public async Task GetMissionList_RequestSuccessful_ReturnMissionInfos()
        {
            var json = "[ { \"AdId\": \"abc\" }, { \"AdId\": \"abc\" } ]";
            _requestor.Setup(g => g.GetRequest(It.IsAny<string>()))
                          .Returns(Task.FromResult(json));

            var result = await _missionService.GetMissionList(It.IsAny<string>());

            Assert.That(result.FirstOrDefault().AdId, Is.EqualTo("abc"));
        }

        [Test]
        public void GetMissionList_IncorrectUri_ThrowHttpRequestException()
        {
            _requestor.Setup(g => g.GetRequest(It.IsAny<string>()))
                        .Throws(new HttpRequestException());

            Assert.That(
                () => _missionService.GetMissionList(It.IsAny<string>()),
                Throws.Exception.TypeOf<HttpRequestException>()
            );
        }

        [Test]
        public async Task SolveMission_RequestSuccessful_ReturnMissionResult()
        {
            var json = "{ \"Success\": \"true\" }";
            _requestor.Setup(g => g.PostRequest(It.IsAny<string>(), null))
                          .Returns(Task.FromResult(json));

            var result = await _missionService.SolveMission(It.IsAny<string>(), It.IsAny<string>());

            Assert.That(result.Success, Is.EqualTo(true));
        }

        [Test]
        public void SolveMission_IncorrectUri_ThrowHttpRequestExceptin()
        {
            _requestor.Setup(g => g.PostRequest(It.IsAny<string>(), null))
                        .Throws(new HttpRequestException());

            Assert.That(
                () => _missionService.SolveMission(It.IsAny<string>(), It.IsAny<string>()),
                Throws.Exception.TypeOf<HttpRequestException>()
            );
        }
    }
}