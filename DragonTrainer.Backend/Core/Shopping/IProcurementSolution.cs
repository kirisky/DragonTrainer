using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.Services;

namespace DragonTrainer.Backend.Core.Shopping
{
    public interface IProcurementSolution
    {
         ShopService ShopService { get; set; }
         UserInfo UserInfo { get; set; }
         void GetItems();
         bool PurchasePotions();
         bool PurchaseLevels();
    }
}