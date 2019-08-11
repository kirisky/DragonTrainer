namespace DragonTrainer.Backend.DTOs
{
    public class GameInfo
    {
        public string GameId { get; set; }
        public int Lives { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public int HighestScore { get; set; }
        public int Turn { get; set; }
    }
}