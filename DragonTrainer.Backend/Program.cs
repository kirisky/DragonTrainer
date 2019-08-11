using DragonTrainer.Backend.Core;
using DragonTrainer.Backend.DTOs;

namespace DragonTrainer.Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.NewGame().Play();
        }
    }
}
