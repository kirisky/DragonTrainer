
using DragonTrainer.Backend.Services;
using DragonTrainer.Backend.Services.Http;
using DragonTrainer.Backend.Core.Missions;
using DragonTrainer.Backend.Core.Shopping;
using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.Helpers;
using System.Net.Http;

namespace DragonTrainer.Backend.Core
{
    public class Game
    {
        private UserInfo _userInfo;
        private Store _store;
        private MissionBoard _missionBoard;

        public static Game NewGame()
        {
            InfoHelper.StartANewGame();
            // initialize logic of Investigation API
            // var investigationService = new InvestigationService(gameRequestor);


            // initialize http requestor
            var gameRequestor = new GameRequestor(new HttpClient());


            // initialize logic of Game API
            var gameService = new GameService(gameRequestor);
            // create a container of user information
            var userInfo = new UserInfo();
            // start a new game
            var result = gameService.StartNewGame().Result;
            // initialize user information with a object-obejct mapper
            var mapper = MapperHelper.Build();
            userInfo = mapper.Map<GameInfo, UserInfo>(result);
            userInfo.TurnsInARound = 0;


            // initialize logic of Shop APIs
            var shopService = new ShopService(gameRequestor);
            // initialize strategy of items buying
            var solution = new NewbieProcurementSolution();
            // initialize logic of store
            var store = new Store(solution, shopService, userInfo);


            // initialize logic of Message APIs
            var missionService = new MissionService(gameRequestor);
            // initialize strategy of task choosing
            var warrior = new NewbieWarrior();
            // initialize logic of mission picking and performing
            var missionBoard = new MissionBoard(warrior, missionService, userInfo);

            return new Game(userInfo, store, missionBoard);
        }

        public Game(UserInfo userInfo, Store store, MissionBoard missionBoard)
        {
            _userInfo = userInfo;
            _store = store;
            _missionBoard = missionBoard;

            _store.RefreshStore();
        }

        public void Play()
        {
            StartGame();

            while (IsStillAlive())
            {
                if (FeelExhausted())
                {
                    InfoHelper.DisplayRecoverHP(_store.RecoverHP());
                }

                // make your level is as high as possible
                _store.LevelUp();
                InfoHelper.DisplayLevelUp();

                _missionBoard.RefreshMissionBoard();
                InfoHelper.DisplayMissionBoardIsRefreshed();

                // if pick nothing, The Missions Board will be refreshed.
                if (!_missionBoard.PickMissions()) continue;

                // when The Mission Board is empty, buy items and refresh the board.
                while (!_missionBoard.MissionBoardIsEmpty())
                {
                    // when the mission is failed, buy items and refresh the board.
                    if (!_missionBoard.PerformTheMission())
                    {
                        IsFailed(); 
                        break;
                    }

                    IsSucceeded();
                }
            }

            EndGame();
        }

        private void StartGame()
        {
            InfoHelper.DisplayGameStartingInfo(_userInfo.GameId);
        }

        private bool IsStillAlive()
        {
            var lives = _userInfo.Lives;
            if (lives == 0) return false;

            InfoHelper.DisplayIsStillAlive(lives);
            return true;
        }

        private bool FeelExhausted()
        {
            var needRecover = _userInfo.Lives == 1 ? true : false;
            InfoHelper.DisplayNeedRecovering(needRecover);

            return needRecover;
        }

        private void IsSucceeded()
        {
            InfoHelper.DisplayMissionResult(
                true, _userInfo.Score,
                _userInfo.Turn, _userInfo.Gold
            );
        }

        private void IsFailed()
        {
            InfoHelper.DisplayMissionResult(
                false, _userInfo.Score,
                _userInfo.Turn, _userInfo.Gold
            );
        }


        private void EndGame()
        {
            InfoHelper.DisplayResultOfTheGame(_userInfo.Score);
        }
    }
}