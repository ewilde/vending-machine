namespace VendingMachine.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class VendingMachineApplication
    {
        private CoinStackCollection coins;

        private HashSet<InventoryItem> inventory;

        public void Load(CoinStackCollection moneyCollection, HashSet<InventoryItem> inventoryItems)
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

        public CoinStackCollection Purchase(Product product, CoinStackCollection coinsOffered)
        {
            if (coinsOffered.Total < product.Price)
            {
                throw new InsufficientFundsException(string.Format("Insufficient funds for {0}.", product.Name), product.Price, coinsOffered.Total);    
            }

            var change = new CoinStackCollection(coinsOffered.Currency);
            var changeRequired = coinsOffered.Total - product.Price;

            foreach (var coin in coinsOffered)
            {
                this.coins.Add(coin);
            }

            foreach (var stack in this.coins.OrderByDescending(item => item.Coin.Denomination))
            {
                if (stack.Amount > 0 && (changeRequired - stack.Coin.Denomination >= 0))
                {
                    while (stack.Amount > 0 && (changeRequired - stack.Coin.Denomination >= 0))
                    {
                        change.Add(stack.Remove());
                        changeRequired = changeRequired - stack.Coin.Denomination;
                    }
                }

                if (changeRequired <= 0)
                {
                    break;
                }
            }

            return change;
        }
    }
}
