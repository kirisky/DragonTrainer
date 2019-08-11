
using DragonTrainer.Backend.Services;
using DragonTrainer.Backend.Services.Http;
using DragonTrainer.Backend.Core.Missions;
using DragonTrainer.Backend.Core.Shopping;
using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.Helpers;
using System;

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
            var gameRequestor = new GameRequestor();

            
            // initialize logic of Game API
            var gameService = new GameService(gameRequestor);
            // create a container of user information
            var userInfo = new UserInfo();
            // start a new game
            var result = gameService.StartNewGame().Result;
            // initialize user information with a object-obejct mapper
            var mapper = MapperHelper.Build();
            userInfo = mapper.Map<GameInfo, UserInfo>(result);


            // initialize logic of Shop APIs
            var shopService = new ShopService(gameRequestor);
            // initialize strategy of items buying
            var solution = new NewbieProcurementSolution();
            solution.ShopService = shopService;
            solution.UserInfo = userInfo;
            // initialize logic of store
            var store = new Store(solution);


            // initialize logic of Message APIs
            var missionService = new MissionService(gameRequestor);
            // initialize strategy of task choosing
            var warrior = new NewbieWarrior();
            warrior.MissionService = missionService;
            warrior.UserInfo = userInfo;
            // initialize logic of mission picking and performing
            var missionBoard = new MissionBoard(warrior);

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
                    InfoHelper.DisplayHaveBuffs(_store.HaveBuffs());
                }

                do
                {
                    if (_missionBoard.MissionBoardIsEmpty()) 
                    {
                        _missionBoard.RefreshMissionBoard();
                        InfoHelper.DisplayMissionBoardIsRefreshed();
                    }

                    _missionBoard.PerformTheBestMission();

                } while (Succeeded()); 
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
            if (lives > 0)
            {
                InfoHelper.DisplayStiilAlive(lives);
                return true;
            }

            return false;
        }

        private bool FeelExhausted()
        {
            var lives = _userInfo.Lives;

            var needRecover = lives > 1 ? false : true;
            InfoHelper.DisplayNeedRecovering(needRecover, lives);

            return needRecover;
        }

        private bool Succeeded()
        {
            InfoHelper.DisplayMissionResult(_userInfo.LastMissionResult);
            return  _userInfo.LastMissionResult;
        }


        private void EndGame()
        {
            InfoHelper.DisplayResultOfTheGame(_userInfo.Score);
        }
    }
}