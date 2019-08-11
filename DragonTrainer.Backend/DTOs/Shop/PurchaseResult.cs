namespace DragonTrainer.Backend.DTOs.Shop
{
    public class PurchaseResult
    {
        public bool ShoppingSuccess { get; set; }
        public int Gold { get; set; }
        public int Lives { get; set; }
        public int Level { get; set; }
        public int Turn { get; set; }
    }
}