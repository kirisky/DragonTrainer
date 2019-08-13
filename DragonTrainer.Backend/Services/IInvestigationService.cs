using System.Threading.Tasks;
using DragonTrainer.Backend.DTOs.Investigation;

namespace DragonTrainer.Backend.Services
{
    public interface IInvestigationService
    {
        Task<Reputation> Investigate(string gameId);    
    }
}