namespace DragonTrainer.Backend.DTOs.Missions
{
    public class MissionResult
    {
        public bool Success { get; set; }
        public int Lives { get; set; }
        public int Gold { get; set; }
        public int Score { get; set; }
        public int HighScore { get; set; }
        public int Turn { get; set; }
        public string Message { get; set; }
    }
}