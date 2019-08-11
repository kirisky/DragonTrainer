using System.Collections.Generic;
using System.Linq;
using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.DTOs.Missions;
using DragonTrainer.Backend.Helpers;
using DragonTrainer.Backend.Services;

namespace DragonTrainer.Backend.Core.Missions
{
    public class NewbieWarrior : IWarrior
    {
        private List<MissionInfo> _missions;

        public UserInfo UserInfo { get; set; } 
        public MissionService MissionService { get; set; }
        public MapperHelper Mapper { get; set; }

        public void GetMissions()
        {
            _missions = MissionService.GetMissionList(UserInfo.GameId).Result;
            SortMissions();
        }

        public bool MissionsIsClean()
        {
            if (_missions == null) return true;
            if (_missions.Any()) return false;

            return true;
        }

        public bool SolveTheTask()
        {
            var taskId = ChooseTheBestTask();
            PerformTheTask(taskId);

            if (!UserInfo.LastMissionResult) return false;

            return true;
        }

        private void SortMissions()
        {
            var missionGroups = 
                from mission in _missions
                group mission by mission.ExpriresIn;

            var sortedGroups = missionGroups.Select(
                group => group.OrderBy(mission => mission.Reward)
            );
                
            var sortedMissions = new List<MissionInfo>();

            foreach (var group in sortedGroups)
            {
                sortedMissions.Add(group.FirstOrDefault());
            }

            _missions = sortedMissions;

        }

        private string ChooseTheBestTask()
        {
            // Using list in a way of queue
            // Why not use queue immediately?
            // It is unnecessary in this case.
            var missionInfo = _missions.FirstOrDefault();
            _missions.Remove(missionInfo);

            return missionInfo.AdId;
        }

        private void PerformTheTask(string missionId)
        {
            var result = MissionService.SovleMission(missionId, UserInfo.GameId).Result;
            // UserInfo.Lives = result.Lives;
            // UserInfo.Gold = result.Gold;
            // UserInfo.Score = result.Score;
            // UserInfo.HighestScore = result.HighScore;
            // UserInfo.Turn = result.Turn;
            // UserInfo.LastMissionResult = result.Success;
            UserInfo = Mapper.Map<MissionResult, UserInfo>(result);

            // write some logs here, like Logger.Log("Finished the task: " + result.Message);
        }
    }
}