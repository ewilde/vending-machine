namespace VendingMachine.Core
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Responsible for managing interactions between the vending machine and user.
    /// </summary>
    public class VendingMachineApplication
    {
        private MoneyHopper hopper;
        private readonly IVendingValidation vendingValidation;
        private readonly IInventoryManager inventoryManager;

        public VendingMachineApplication() : this(new VendingValidation(), new InventoryManager())
        {
        }

        public VendingMachineApplication(IVendingValidation vendingValidation, IInventoryManager inventoryManager)
        {
            this.vendingValidation = vendingValidation;
            this.inventoryManager = inventoryManager;
        }

        /// <summary>
        /// Gets the vending machine balance.
        /// </summary>
        public decimal Balance
        {
            get
            {
                return this.hopper.Total;
            }
        }

        /// <summary>
        /// Gets the list of inventory for this vending machine.
        /// </summary>
        public IEnumerable<InventoryItem> Inventory
        {
            get
            {
                return this.inventoryManager.Inventory;
            }
        }

        /// <summary>
        /// Loads the vending machine with the specified money and inventory items.
        /// </summary>
        public void Load(MoneyHopper money, List<InventoryItem> inventoryItems)
        {
            this.hopper = money;
            this.inventoryManager.Load(inventoryItems);
        }

        /// <summary>
        /// Purchases the product at the specified location.
        /// </summary>
        /// <param name="location">The location of the product to be purchased.</param>
        /// <param name="customersPurse">The coins offered by the customer to purchase their choosen item.</param>
        /// <returns>
        /// A collection of coins making up the change, in the case where exact change was not given.
        /// </returns>
        /// <exception cref="InsufficientFundsException">thrown when the specified coins are not enough to make a purchase.</exception>
        /// <exception cref="OutOfStockException">thrown when the product being purchased is out of stock.</exception>
        /// <exception cref="ExactChangeRequiredException">thrown when the vending machine does not have enough coins to return the correct amount of change.</exception>
        public CoinPurse Purchase(string location, CoinPurse customersPurse)
        {
            return this.Purchase(this.inventoryManager.GetInventory(location).Product, customersPurse);
        }

        public CoinPurse Purchase(Product product, CoinPurse coinsOffered)
        {
            this.vendingValidation.EnsureSufficientCoinsGiven(product, coinsOffered);
            this.vendingValidation.EnsureProductIsInStock(this.inventoryManager.GetInventory(product));

            this.AcceptCoins(coinsOffered);

            var change = this.CalculateChange(product, coinsOffered);

            this.inventoryManager.UpdateInventory(product);

            return change;
        }

        /// <summary>
        /// Calculates the change required to be handed back based on the product being purchased.
        /// </summary>
        protected CoinPurse CalculateChange(Product product, CoinPurse coinsOffered)
        {
            var change = new CoinPurse(coinsOffered.Currency);
            var changeRequired = coinsOffered.Total - product.Price;
            foreach (var stack in this.hopper.OrderByDescending(item => item.Coin.Denomination))
            {               
                while (stack.Amount > 0 && (changeRequired - stack.Coin.Denomination >= 0))
                {
                    change.Add(stack.Remove());
                    changeRequired = changeRequired - stack.Coin.Denomination;
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
    }
}
