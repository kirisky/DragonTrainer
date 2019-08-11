using DragonTrainer.Backend.Helpers;

namespace DragonTrainer.Backend.Core.Missions
{
    public class MissionBoard
    {
        private IWarrior _warrior;

        public MissionBoard(IWarrior warrior)
        {
            _warrior = warrior; 
        }

        public void RefreshMissionBoard()
        {
            _warrior.GetMissions();
        }

        public bool MissionBoardIsEmpty()
        {
            return _warrior.MissionsIsClean();
        }

        public bool PerformTheBestMission()
        {
            return _warrior.SolveTheTask();
        }
    }
}