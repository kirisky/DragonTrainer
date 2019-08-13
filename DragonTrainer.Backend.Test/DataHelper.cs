using System.Collections.Generic;
using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.DTOs.Shop;

namespace DragonTrainer.Backend.Test
{
    public class DataHelper
    {
        public static UserInfo GetUserInfo()
        {
            return new UserInfo {
                GameId = "123",
                Lives = 3,
                Gold = 100,
                Level = 10,
                Score = 120,
                HighestScore = 10000,
                Turn = 30,
                TurnsInARound = 4,
                PeopleReputation = 0,
                StateReputation = 0,
                UnderWorldReputation = 0,
                LastMissionResult = true
            };
        }

        public static List<ItemInfo> GetItemInfoList()
        {
            return new List<ItemInfo>
            {
                new ItemInfo { 
                    Id = "hpot",
                    Name = "Healing potion",
                    Cost = 50
                },

                new ItemInfo { 
                    Id = "cs",
                    Name = "Claw Sharpening",
                    Cost = 100
                },

                new ItemInfo { 
                    Id = "wingpotmax",
                    Name = "Potion of Awesome Wings",
                    Cost = 300
                },
            };
        }
    }
}