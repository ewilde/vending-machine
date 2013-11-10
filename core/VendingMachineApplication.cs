namespace VendingMachine.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class VendingMachineApplication
    {
        private CoinCollection coins;

        private HashSet<InventoryItem> inventory;

        public void Load(CoinCollection moneyCollection, HashSet<InventoryItem> inventoryItems)
        {
            this.coins = moneyCollection;
            this.inventory = inventoryItems;
        }

        public decimal TotalMoneyAvailable
        {
            get
            {
                return this.coins.Total;
            }
        }

        public IEnumerable<InventoryItem> Inventory
        {
            get
            {
                return this.inventory;
            }            
        }

        public CoinCollection Purchase(Product product, CoinCollection coins)
        {
            return new CoinCollection(Currency.GBP);
        }
    }
}
