using System.Threading.Tasks;
using DragonTrainer.Backend.DTOs;

namespace DragonTrainer.Backend.Services
{
    public interface IGameService
    {
        Task<GameInfo> StartNewGame();    
    }
}