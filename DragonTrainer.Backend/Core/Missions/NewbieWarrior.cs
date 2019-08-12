using System;
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
            UserInfo.TurnsInARound = 0;
        }

        public bool MissionsIsClean()
        {
            if (_missions == null) return true;
            if (!_missions.Any()) return true;

            return false;
        }

        public bool PickMissions()
        {
            // pick sure thing
            var sureThings = _missions
                .Where(m => m.Probability == ProbabilityHelper.SureThing)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.SureThingFactor));

            // pick piece of cake
            var pieceOfCakes = _missions
                .Where(m => m.Probability == ProbabilityHelper.PieceOfCake)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.PieceOfCakeFactor));

            // pick walk in the park
            var walkInTheParks = _missions
                .Where(m => m.Probability == ProbabilityHelper.WalkInThePark)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.WalkInTheParkFactor));

            // pick quite likely
            var quiteLikely = _missions
                .Where(m => m.Probability == ProbabilityHelper.QuiteLikely)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.QuiteLikelyFactor));

            var hmmm = _missions
                .Where(m => m.Probability == ProbabilityHelper.Hmmm)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.HmmmFactor));

            var ratherDetrimental = _missions
                .Where(m => m.Probability == ProbabilityHelper.RatherDetrimetal)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.RatherDetrimetalFactor));

            var risky = _missions
                .Where(m => m.Probability == ProbabilityHelper.Risky)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.RiskyFactor));

            var gamble = _missions
                .Where(m => m.Probability == ProbabilityHelper.Gamble)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.GambleFactor));

            var playingWithFire = _missions
                .Where(m => m.Probability == ProbabilityHelper.PlayWithFire)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.PlayWithFireFactor));

            var suicideMission = _missions
                .Where(m => m.Probability == ProbabilityHelper.SuicideMission)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.SuicideMissionFactor));

            _missions = new List<MissionInfo>();
            MergeIntoMissions(sureThings);
            MergeIntoMissions(pieceOfCakes);
            MergeIntoMissions(walkInTheParks);
            MergeIntoMissions(quiteLikely);
            MergeIntoMissions(hmmm);
            MergeIntoMissions(ratherDetrimental);
            MergeIntoMissions(risky);
            MergeIntoMissions(gamble);
            MergeIntoMissions(playingWithFire);
            MergeIntoMissions(suicideMission);

            return _missions.Any();
        }

        public bool SolveTheTask()
        {
            var task = ChooseTheBestTask();
            if (task == null) return false;

            PerformTheTask(task.AdId);
            return UserInfo.LastMissionResult;
        }

        private MissionInfo ChooseTheBestTask()
        {
            do {
                var mission = _missions.FirstOrDefault();
                _missions.Remove(mission);

                if (mission.ExpiresIn > UserInfo.TurnsInARound)
                {
                    InfoHelper.DisplayMissionInfo(mission.ExpiresIn, UserInfo.Lives, UserInfo.TurnsInARound, UserInfo.Score, UserInfo.Gold);

                    return mission;
                }

            } while(_missions.Any());

            return null;
        }

        private void PerformTheTask(string missionId)
        {
            var result = MissionService.SovleMission(UserInfo.GameId, missionId).Result;

            UserInfo.Lives = result.Lives;
            UserInfo.Gold = result.Gold;
            UserInfo.Score = result.Score;
            UserInfo.HighestScore = result.HighScore;
            UserInfo.Turn = result.Turn;
            UserInfo.LastMissionResult = result.Success;
            UserInfo.TurnsInARound += 1;
            // UserInfo = Mapper.Map<MissionResult, UserInfo>(result);

            // write some logs here, like Logger.Log("Finished the task: " + result.Message);
        }

        private void MergeIntoMissions(IEnumerable<MissionInfo> missions)
        {
            foreach (var mission in missions)
            {
                _missions.Add(mission); 
            }
        }

        private int GetFactor(int factor)
        {
            // Here is a convenience for modification algorithm in the later time.
            var factorNumber = 0;
            return factorNumber == 0 ? factor : factorNumber;
        }
    }
}