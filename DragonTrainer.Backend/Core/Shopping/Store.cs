namespace DragonTrainer.Backend.Core.Shopping
{
    public class Store
    {
        private IProcurementSolution _solution;

        public Store(IProcurementSolution solution)
        {
            _solution = solution;
        }

        public void RefreshStore()
        {
            _solution.GetItems();
        }

        public bool RecoverHP()
        {
            return _solution.PurchasePotions();
        }

        public bool HaveBuffs()
        {
            return _solution.PurchaseBuffs();
        }
    }
}