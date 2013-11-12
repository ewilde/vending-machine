namespace VendingMachine.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class VendingMachineApplication
    {
        private MoneyHopper hopper;

        private List<InventoryItem> inventory;

        public void Load(MoneyHopper hopper, List<InventoryItem> inventoryItems)
        {
            this.hopper = hopper;
            this.inventory = inventoryItems;
        }

        public decimal TotalMoneyAvailable
        {
            get
            {
                return this.hopper.Total;
            }
        }

        public IEnumerable<InventoryItem> Inventory
        {
            get
            {
                return this.inventory;
            }            
        }

        public CoinPurse Purchase(string location, CoinPurse customersPurse)
        {
            return this.Purchase(this.GetInventory(location).Product, customersPurse);
        }

        public CoinPurse Purchase(Product product, CoinPurse coinsOffered)
        {
            this.EnsureSufficientCoinsGiven(product, coinsOffered);

            this.AcceptCoins(coinsOffered);

            var change = this.CalculateChange(product, coinsOffered);

            this.UpdateInventory(product);

            return change;
        }

        protected void UpdateInventory(Product product)
        {
            this.GetInventory(product).Quantity -= 1;
        }

        private InventoryItem GetInventory(Product product)
        {
            return this.Inventory.First(item => item.Product.Equals(product));
        }

        private InventoryItem GetInventory(string location)
        {
            return this.Inventory.First(item => item.MachineLocation.Equals(location));
        }

        private CoinPurse CalculateChange(Product product, CoinPurse coinsOffered)
        {
            var change = new CoinPurse(coinsOffered.Currency);
            var changeRequired = coinsOffered.Total - product.Price;
            foreach (var stack in this.hopper.OrderByDescending(item => item.Coin.Denomination))
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

            if (changeRequired > 0)
            {
                this.ReturnCoins(coinsOffered);
                throw new ExactChangeRequiredException("Vending machine is low on change, please provide exact change.");
            }

            coinsOffered.Clear();
            return change;
        }

        private void ReturnCoins(IEnumerable<StackOfCoins> coinsOffered)
        {
            foreach (var coin in coinsOffered)
            {
                this.hopper.Remove(coin);
            }
        }

        private void AcceptCoins(IEnumerable<StackOfCoins> coinsOffered)
        {
            foreach (var coin in coinsOffered)
            {
                this.hopper.Add(coin);
            }
        }

        private void EnsureSufficientCoinsGiven(Product product, MoneyHopper coinsOffered)
        {
            if (coinsOffered.Total < product.Price)
            {
                throw new InsufficientFundsException(
                    string.Format("Insufficient funds for {0}.", product.Name), product.Price, coinsOffered.Total);
            }
        }
    }
}
