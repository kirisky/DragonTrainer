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

        public static void DisplayNeedRecovering(bool need)
        {
            if (need) Say($"I only have [1]HP now. I need recovering!");
        }

        public static void DisplayIsStillAlive(int lives)
        {
            Say($"I have [{lives}]HP now. So, I am still alive.");
        }

        public static void DisplayResultOfTheGame(int score)
        {
            var msg = score > 1000 ? "You Won the Game!" : "You Lost the Game!";
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

        public static void DisplayLevelUp(bool result)
        {
            var msg = result ? "succeeded" : "failed";
            Say($"Level up is {msg}!");
        }

        public static void DisplayMissionResult(bool result, int score, int turn, int gold)
        {
            var msg = result ? "complete" : "failed";
            Say($"the mission is {msg}! ");
            Say(
                $"Current Score: {score}, " +
                $"Current Gold: {gold}, " +
                $"Current Turn: {turn}."
            );
        }

        public static void DisplayMissionInfo(int expiresIn, int lives, int turns, int score, int gold)
        {
            Say(
                $"ExpiresIn: {expiresIn}, " +
                $"Lives: {lives} " +
                $"Turn: {turns}, " +
                $"Score: {score}, " +
                $"Gold: {gold}"
            );
        }

        private static void Say(string msg)
        {
            Console.WriteLine(msg);
        }

    }
}