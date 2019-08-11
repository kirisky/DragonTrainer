using System;

namespace DragonTrainer.Backend.Helpers
{
    public class InfoHelper
    {
        public static void StartANewGame()
        {
            Say("Now, Start a new game!");
        }

        public static void DisplayGameStartingInfo(string gameId)
        {
            Say($"Now, the game({gameId}) is starting!");
            Say("Start your battle!");
        }

        public static void DisplayNeedRecovering(bool need, int lives)
        {
            if(need) Say($"I only have [{lives}]HP now. I need recovering!");
        }

        public static void DisplayStiilAlive(int lives)
        {
            Say($"I have [{lives}]HP now. So, don't worry.");
        }

        public static void DisplayResultOfTheGame(int score)
        {
            var msg = score > 1000  ? "You Won the Game!" : "You Lost the Game!";
            Say(msg + $" Your Score is {score}.");
        }

        public static void DisplayMissionBoardIsRefreshed()
        {
            Say("Mission board is refreshed now, please continue your job!");
        }

        public static void DisplayRecoverHP(bool result)
        {
            var msg = result ? "succeeded" : "failed";
            Say($"Recovering HP is {msg}!");
        }

        public static void DisplayHaveBuffs(bool result)
        {
            var msg = result ? "succeeded" : "failed";
            Say($"Adding buff is {msg}!");
        }

        public static void DisplayMissionResult(bool result)
        {
            var msg = result ? "complete" : "failed";
            Say("the mission is {msg}!");
        }

        private static void Say(string msg)
        {
            Console.WriteLine(msg);
        }
        
    }
}