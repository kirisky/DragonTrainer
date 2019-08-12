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
        
        public bool PickMissions()
        {
            return _warrior.PickMissions();
        }

        public bool PerformTheMission()
        {
            return _warrior.SolveTheTask();
        }
    }
}