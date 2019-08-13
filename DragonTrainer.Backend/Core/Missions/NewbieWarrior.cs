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
        public List<MissionInfo> GetMissions(IMissionService missionService, string gameId)
        {
            return missionService.GetMissionList(gameId).Result;
        }

        public bool MissionsIsClean(List<MissionInfo> missions)
        {
            if (missions == null) return true;
            if (!missions.Any()) return true;

            return false;
        }

        public List<MissionInfo> PickMissions(List<MissionInfo> missions)
        {
            // pick sure thing
            var sureThings = missions
                .Where(m => m.Probability == ProbabilityHelper.SureThing)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.SureThingFactor));

            // pick piece of cake
            var pieceOfCakes = missions
                .Where(m => m.Probability == ProbabilityHelper.PieceOfCake)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.PieceOfCakeFactor));

            // pick walk in the park
            var walkInTheParks = missions
                .Where(m => m.Probability == ProbabilityHelper.WalkInThePark)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.WalkInTheParkFactor));

            // pick quite likely
            var quiteLikely = missions
                .Where(m => m.Probability == ProbabilityHelper.QuiteLikely)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.QuiteLikelyFactor));

            var hmmm = missions
                .Where(m => m.Probability == ProbabilityHelper.Hmmm)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.HmmmFactor));

            var ratherDetrimental = missions
                .Where(m => m.Probability == ProbabilityHelper.RatherDetrimetal)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.RatherDetrimetalFactor));

            var risky = missions
                .Where(m => m.Probability == ProbabilityHelper.Risky)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.RiskyFactor));

            var gamble = missions
                .Where(m => m.Probability == ProbabilityHelper.Gamble)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.GambleFactor));

            var playingWithFire = missions
                .Where(m => m.Probability == ProbabilityHelper.PlayWithFire)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.PlayWithFireFactor));

            var suicideMission = missions
                .Where(m => m.Probability == ProbabilityHelper.SuicideMission)
                .Where(m => m.Reward > GetFactor(ProbabilityHelper.SuicideMissionFactor));

            var missionsContainer = new List<MissionInfo>();
            MergeMissions(missionsContainer, sureThings);
            MergeMissions(missionsContainer, pieceOfCakes);
            MergeMissions(missionsContainer, walkInTheParks);
            MergeMissions(missionsContainer, quiteLikely);
            MergeMissions(missionsContainer, hmmm);
            MergeMissions(missionsContainer, ratherDetrimental);
            MergeMissions(missionsContainer, risky);
            MergeMissions(missionsContainer, gamble);
            MergeMissions(missionsContainer, playingWithFire);
            MergeMissions(missionsContainer, suicideMission);

            return missionsContainer;
        }

        public MissionInfo ChooseTheBestMission(List<MissionInfo> missions, UserInfo userInfo)
        {
            do {
                var mission = missions.FirstOrDefault();
                missions.Remove(mission);

                if (mission.ExpiresIn > userInfo.TurnsInARound)
                {
                    return mission;
                }

            } while(missions.Any());

            return null;
        }

        public MissionResult PerformTheMission(IMissionService missionService, string gameId, string missionId)
        {
            return missionService.SolveMission(gameId, missionId).Result;
        }

        private void MergeMissions(List<MissionInfo> missionsContainer, IEnumerable<MissionInfo> missions)
        {
            foreach (var mission in missions)
            {
                missionsContainer.Add(mission); 
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