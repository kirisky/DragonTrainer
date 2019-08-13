using System.Collections.Generic;
using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.DTOs.Missions;
using DragonTrainer.Backend.Services;

namespace DragonTrainer.Backend.Core.Missions
{
    public interface IWarrior
    {
        List<MissionInfo> GetMissions(IMissionService missionService, string gameId);
        bool MissionsIsClean(List<MissionInfo> missions);
        List<MissionInfo> PickMissions(List<MissionInfo> missions);
        MissionInfo ChooseTheBestMission(List<MissionInfo> missions, UserInfo userInfo);
        MissionResult PerformTheMission(IMissionService missionService, string gameId, string adId);
    }
}