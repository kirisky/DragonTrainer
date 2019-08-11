namespace DragonTrainer.Backend.DTOs
{
    public class UserInfo
    {
        public string GameId { get; set; }
        public int Lives { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public int HighestScore { get; set; }
        public int Turn { get; set; }
        public int PeopleReputation { get; set; }
        public int StateReputation { get; set; }
        public int UnderWorldReputation { get; set; }
        public bool LastMissionResult { get; set; }

    }
}