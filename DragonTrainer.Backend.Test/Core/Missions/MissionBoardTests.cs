using System.Collections.Generic;
using System.Linq;
using DragonTrainer.Backend.Core.Missions;
using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.DTOs.Missions;
using DragonTrainer.Backend.Services;
using Moq;
using NUnit.Framework;

namespace DragonTrainer.Backend.Test.Core.Missions
{
    [TestFixture]
    public class MissionBoardTests
    {
        private Mock<IWarrior> _warrior;
        private Mock<IMissionService> _missionService;
        private MissionBoard _missionBoard;

        [SetUp]
        public void SetUp()
        {
            _warrior = new Mock<IWarrior>(); 
            _missionService = new Mock<IMissionService>();
            _missionBoard = new MissionBoard(
                _warrior.Object, _missionService.Object, DataHelper.GetUserInfo());
        }

        [Test]
        public void RefreshMissionBoard_CalledTheGetMissions()
        {
            _missionBoard.RefreshMissionBoard();
            _warrior.Verify(w => w.GetMissions(_missionService.Object, 
                            It.IsAny<string>())); 
        }

        [Test]
        public void MissionBoardIsEmpty_IsEmpty_ReturnTrue()
        {
            _warrior.Setup(w => w.MissionsIsClean(It.IsAny<List<MissionInfo>>()))
                    .Returns(true);

            var result = _missionBoard.MissionBoardIsEmpty();

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void MissionBoardIsEmpty_IsNotEmpty_ReturnFalse()
        {
            _warrior.Setup(w => w.MissionsIsClean(It.IsAny<List<MissionInfo>>()))
                    .Returns(false);

            var result = _missionBoard.MissionBoardIsEmpty();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void PickMissions_PickSomeMissions_ReturnTrue()
        {
            var missions = new List<MissionInfo>();
            missions.Add(new MissionInfo());

            _warrior.Setup(w => w.PickMissions(It.IsAny<List<MissionInfo>>()))
                    .Returns(missions);

            var result = _missionBoard.PickMissions();

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void PickMissions_PickNothing_ReturnFalse()
        {
            var missions = new List<MissionInfo>();

            _warrior.Setup(w => w.PickMissions(It.IsAny<List<MissionInfo>>()))
                    .Returns(missions);

            var result = _missionBoard.PickMissions();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void PerformTheMission_MissionIsNull_ReturnFalse()
        {
            _warrior.Setup(w => w.ChooseTheBestMission(
                                    It.IsAny<List<MissionInfo>>(), 
                                    It.IsAny<UserInfo>()))
                    .Returns(value: null);

            var result = _missionBoard.PerformTheMission();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void PerformTheMission_MissionSuccessful_ReturnTrue()
        {
            _warrior.Setup(w => w.ChooseTheBestMission(
                                    It.IsAny<List<MissionInfo>>(), 
                                    It.IsAny<UserInfo>()))
                    .Returns(new MissionInfo());

            var missionResult = new MissionResult {
                Success = true
            };
            _warrior.Setup(w => w.PerformTheMission(
                                    It.IsAny<IMissionService>(), 
                                    It.IsAny<string>(),
                                    It.IsAny<string>()))
                    .Returns(missionResult);


            var result = _missionBoard.PerformTheMission();

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void PerformTheMission_MissionFailed_ReturnFalse()
        {
            _warrior.Setup(w => w.ChooseTheBestMission(
                                    It.IsAny<List<MissionInfo>>(), 
                                    It.IsAny<UserInfo>()))
                    .Returns(new MissionInfo());

            var missionResult = new MissionResult {
                Success = false
            };
            _warrior.Setup(w => w.PerformTheMission(
                                    It.IsAny<IMissionService>(), 
                                    It.IsAny<string>(),
                                    It.IsAny<string>()))
                    .Returns(missionResult);


            var result = _missionBoard.PerformTheMission();

            Assert.That(result, Is.EqualTo(false));
        }
        

    }
}