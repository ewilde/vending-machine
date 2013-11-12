namespace VendingMachine.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class VendingMachineApplication
    {
        private MoneyHopper coins;

        private List<InventoryItem> inventory;

        public void Load(MoneyHopper moneyCollection, List<InventoryItem> inventoryItems)
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

        public MoneyHopper Purchase(Product product, MoneyHopper coinsOffered)
        {
            this.EnsureSufficientCoinsGiven(product, coinsOffered);

            this.AcceptCoins(coinsOffered);

            var change = this.CalculateChange(product, coinsOffered);
            
            return change;
        }

        private MoneyHopper CalculateChange(Product product, MoneyHopper coinsOffered)
        {
            var change = new MoneyHopper(coinsOffered.Currency);
            var changeRequired = coinsOffered.Total - product.Price;
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
                this.coins.Remove(coin);
            }
        }

        private void AcceptCoins(IEnumerable<StackOfCoins> coinsOffered)
        {
            foreach (var coin in coinsOffered)
            {
                this.coins.Add(coin);
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
