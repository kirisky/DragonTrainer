using System.Collections.Generic;
using System.Threading.Tasks;
using DragonTrainer.Backend.DTOs.Missions;

namespace DragonTrainer.Backend.Services
{
    public interface IMissionService
    {
        Task<List<MissionInfo>> GetMissionList(string gameId);
        Task<MissionResult> SolveMission(string gameId, string missionId);
    }
}