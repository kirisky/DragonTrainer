using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DragonTrainer.Backend.Core.Missions;
using DragonTrainer.Backend.DTOs.Missions;
using DragonTrainer.Backend.Services;
using Moq;
using NUnit.Framework;

namespace DragonTrainer.Backend.Test.Core.Missions
{
    [TestFixture]
    public class NewbieWarriorTests
    {
        private Mock<IMissionService> _missionService;
        private NewbieWarrior _newbieWarrior;
        private List<MissionInfo> _missions;

        [SetUp]
        public void SetUp()
        {
            _missionService = new Mock<IMissionService>();

            _newbieWarrior = new NewbieWarrior();

            _missions = new List<MissionInfo>();

        }

        [Test]
        public void GetMissions_FetchSuccessful_ReturnMissions()
        {
            _missions.Add(new MissionInfo());

            _missionService.Setup(m => m.GetMissionList(It.IsAny<string>()))
                           .Returns(Task.FromResult(_missions));

            var result = _newbieWarrior.GetMissions(
                            _missionService.Object, It.IsAny<string>());

            _missionService.Verify(m => m.GetMissionList(It.IsAny<string>()));
            Assert.That(result.Any(), Is.EqualTo(true));
        }

        [Test]
        public void GetMissions_FetchFailed_ReturnEmptyContainer()
        {
            _missionService.Setup(m => m.GetMissionList(It.IsAny<string>()))
                           .Returns(Task.FromResult(_missions));

            var result = _newbieWarrior.GetMissions(
                            _missionService.Object, It.IsAny<string>());

            _missionService.Verify(m => m.GetMissionList(It.IsAny<string>()));
            Assert.That(result.Any(), Is.EqualTo(false));
        }

        [Test]
        public void MissionsIsClean_MissionsIsNull_ReturnTrue()
        {
            var result = _newbieWarrior.MissionsIsClean(null);
            
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void MissionsIsClean_IsClean_ReturnTrue()
        {
            var result = _newbieWarrior.MissionsIsClean(_missions);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void MissionsIsClean_IsNotClean_ReturnFalse()
        {
            _missions.Add(new MissionInfo());
            var result = _newbieWarrior.MissionsIsClean(_missions);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void PickMissions_PickSuccessful_ReturnMissions()
        {
            _missions.Add(new MissionInfo {
                Probability = "Sure thing",
                Reward = 100
            });

            var result = _newbieWarrior.PickMissions(_missions);

            Assert.That(result.Any(), Is.EqualTo(true)); 
        }

        [Test]
        public void PickMissions_PickFailed_ReturnEmptyContainer()
        {
            var result = _newbieWarrior.PickMissions(_missions);

            Assert.That(result.Any(), Is.EqualTo(false)); 
        }

        [Test]
        public void ChooseTheBestMission_ChooseOne_ReturnMissionInfo()
        {
            var userInfo = DataHelper.GetUserInfo();
            userInfo.TurnsInARound = 1;

            _missions.Add(new MissionInfo {
                ExpiresIn = 7
            });

            var result = _newbieWarrior.ChooseTheBestMission(_missions, userInfo);

            Assert.That(result.ExpiresIn, Is.EqualTo(7));
        }

        [Test]
        public void ChooseTheBestMission_ChooseNothing_ReturnNull()
        {
            var userInfo = DataHelper.GetUserInfo();
            userInfo.TurnsInARound = 7;

            _missions.Add(new MissionInfo {
                ExpiresIn = 1
            });

            var result = _newbieWarrior.ChooseTheBestMission(_missions, userInfo);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void PerformTheMission_MissionSuccessful_ReturnMissionResult()
        {
            var missionResult = new MissionResult()
            {
                Success = true
            };
            _missionService.Setup(m => m.SolveMission(
                                            It.IsAny<string>(), 
                                            It.IsAny<string>()))
                            .Returns(Task.FromResult(missionResult));

            var result = _newbieWarrior.PerformTheMission(
                                                _missionService.Object, 
                                                It.IsAny<string>(), 
                                                It.IsAny<string>());

            Assert.That(result.Success, Is.EqualTo(true));
        }

        [Test]
        public void PerformTheMission_MissionFailed_ReturnMissionResult()
        {
            var missionResult = new MissionResult()
            {
                Success = false
            };
            _missionService.Setup(m => m.SolveMission(
                                            It.IsAny<string>(), 
                                            It.IsAny<string>()))
                            .Returns(Task.FromResult(missionResult));

            var result = _newbieWarrior.PerformTheMission(
                                                _missionService.Object, 
                                                It.IsAny<string>(), 
                                                It.IsAny<string>());

            Assert.That(result.Success, Is.EqualTo(false));
        }

        [Test]
        public void PerformTheMission_IncorrectArguments_ThrowException()
        {
            _missionService.Setup(m => m.SolveMission(
                                            It.IsAny<string>(), 
                                            It.IsAny<string>()))
                            .Throws(new HttpRequestException());

            Assert.That(
                () => _newbieWarrior.PerformTheMission(
                    _missionService.Object, It.IsAny<string>(), It.IsAny<string>()
                ),
                Throws.Exception.TypeOf<HttpRequestException>()
            );
        }


    }
}