using System.Net.Http;
using System.Threading.Tasks;
using DragonTrainer.Backend.Services;
using DragonTrainer.Backend.Services.Http;
using Moq;
using NUnit.Framework;

namespace DragonTrainer.Backend.Test.Services
{
    [TestFixture]
    public class InvestigationServiceTests
    {
        private Mock<IGameRequestor> _requestor;
        private InvestigationService _investigation;

        [SetUp]
        public void SetUp()
        {
            _requestor = new Mock<IGameRequestor>();
            _investigation = new InvestigationService(_requestor.Object);
        }

        [Test] 
        public async Task Investigate_GetReputations_ReturnReputationInfo()
        {
            var json = "{ \"people\": 10 }";
            _requestor.Setup(i => i.PostRequest(It.IsAny<string>(), null))
                          .Returns(Task.FromResult(json));

            var result = await _investigation.Investigate(It.IsAny<string>());

            Assert.That(result.People, Is.EqualTo(10));
        }

        [Test] 
        public void Investigate_IncorrectUri_ThrowHttpRequestException()
        {
            _requestor.Setup(i => i.PostRequest(It.IsAny<string>(), null))
                        .Throws(new HttpRequestException());

            Assert.That(
                () => _investigation.Investigate(It.IsAny<string>()),
                Throws.Exception.TypeOf<HttpRequestException>()
            );
        }
    }
}