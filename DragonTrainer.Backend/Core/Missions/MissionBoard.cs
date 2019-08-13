using System.Collections.Generic;
using System.Linq;
using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.DTOs.Missions;
using DragonTrainer.Backend.Helpers;
using DragonTrainer.Backend.Services;

namespace DragonTrainer.Backend.Core.Missions
{
    public class MissionBoard
    {
        private IWarrior _warrior;
        private IMissionService _missionService;
        private UserInfo _userInfo;
        private List<MissionInfo> _missions;

        public MissionBoard(IWarrior warrior, IMissionService missionService, UserInfo userInfo)
        {
            _warrior = warrior; 
            _missionService = missionService;
            _userInfo = userInfo;
        }

        public void RefreshMissionBoard()
        {
            _missions = _warrior.GetMissions(_missionService, _userInfo.GameId);
            _userInfo.TurnsInARound = 0;
        }

        public bool MissionBoardIsEmpty()
        {
            return _warrior.MissionsIsClean(_missions);
        }
        
        public bool PickMissions()
        {
            return _warrior.PickMissions(_missions).Any();
        }

        public bool PerformTheMission()
        {
            var mission = _warrior.ChooseTheBestMission(_missions, _userInfo);
            if (mission == null) return false;

            var missionResult = _warrior.PerformTheMission(
                _missionService, _userInfo.GameId, mission.AdId
            );

            _userInfo.Lives = missionResult.Lives;
            _userInfo.Gold = missionResult.Gold;
            _userInfo.Score = missionResult.Score;
            _userInfo.HighestScore = missionResult.HighScore;
            _userInfo.Turn = missionResult.Turn;
            _userInfo.LastMissionResult = missionResult.Success;
            _userInfo.TurnsInARound += 1;

            return _userInfo.LastMissionResult;
        }
    }
}