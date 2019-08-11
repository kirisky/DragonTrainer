using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.Services;

namespace DragonTrainer.Backend.Core.Missions
{
    public interface IWarrior
    {
        UserInfo UserInfo { get; set; } 
        MissionService MissionService { get; set; }

        void GetMissions();
        bool MissionsIsClean();
        bool SolveTheTask();
    }
}