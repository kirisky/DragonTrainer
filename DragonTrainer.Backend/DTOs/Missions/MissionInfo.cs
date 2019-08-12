namespace DragonTrainer.Backend.DTOs.Missions
{
    public class MissionInfo
    {
        public string AdId { get; set; }
        public string Message { get; set; }
        public int Reward { get; set; }
        public int ExpiresIn { get; set; }
        public string Probability { get; set; }
    }
}