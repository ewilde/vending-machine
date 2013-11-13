namespace VendingMachine.Console
{
    using System;
    using System.Collections.Generic;

    using VendingMachine.Core;

    class Program
    {
        private VendingMachineApplication vendingMachine;

        private CoinPurse customersPurse;

        static void Main()
        {
            Console.WriteLine("Press Q to quit....");
            new Program().Run();
        }

        public Program()
        {
            this.StartOfDayLoad();
            this.InitializeCustomerData();
        }

        private void Run()
        {
            string key = string.Empty;
            bool errorOccured = false;
            do
            {
                this.PrintState();
                this.PrintPrompt();

                key = ReadInput(key);
                if (key != "Q")
                {
                    errorOccured  = !this.PurchaseItem(key);
                }
            }
            while (key != "Q" && !errorOccured);

        }

        private Action lastActionMessage;
        private bool PurchaseItem(string key)
        {
            try
            {
                var change = this.vendingMachine.Purchase(key, this.customersPurse);
                this.customersPurse = change;
                lastActionMessage = () => ConsoleUtility.PrintSuccess("Item purchased...");

                return true;
            }
            catch (VendingMachineException vendingException)
            {
                lastActionMessage = () => ConsoleUtility.PrintError(vendingException.Message);
                return true;
            }
            catch (Exception generalException)
            {
                ConsoleUtility.PrintFatal(generalException.Message);
                return false;
            }
        }

        private void PrintPrompt()
        {
            Console.Write("Please enter location to purchase...: ");
        }

        private void PrintState()
        {
            Console.Clear();
           

            Console.WriteLine("----- Vending machine state -------");
            Console.WriteLine(
                this.vendingMachine.Inventory.ToStringTable(
                    new[] { "Slot", "Item", "Price", "Stock" },
                    item => item.MachineLocation,
                    item => item.Product.Name,
                    item => string.Format("£ {0:0.00}", item.Product.Price),
                    item => item.Quantity));

            Console.WriteLine("----- Customers purse -------");
            Console.WriteLine(
                this.customersPurse.ToStringTable(
                    new[] { "Currency", "Denomination", "Number of"},
                    item => item.Coin.Currency,
                    item => item.Coin.Denomination,
                    item => item.Amount));

            if (this.lastActionMessage != null)
            {
                this.lastActionMessage.Invoke();
                this.lastActionMessage = null;
            }

            Console.WriteLine();
            Console.WriteLine(string.Format("{0,-15}£ {1:0.00}", "Purse total:", this.customersPurse.Total));
        }

        private string ReadInput(string key)
        {
            var readLine = Console.ReadLine();
            if (readLine != null)
            {
                key = readLine.ToUpper();
            }
            return key;
        }

        private void InitializeCustomerData()
        {
            this.customersPurse = new CoinPurse(Currency.GBP)
                {
                    new StackOfCoins(new Coin(Currency.GBP, 0.01m), 3),
                    new StackOfCoins(new Coin(Currency.GBP, 0.02m), 4),
                    new StackOfCoins(new Coin(Currency.GBP, 0.05m), 8),
                    new StackOfCoins(new Coin(Currency.GBP, 0.10m), 2),
                    new StackOfCoins(new Coin(Currency.GBP, 0.20m), 5),
                    new StackOfCoins(new Coin(Currency.GBP, 0.50m), 3),
                    new StackOfCoins(new Coin(Currency.GBP, 1.00m), 2),
                    new StackOfCoins(new Coin(Currency.GBP, 2.00m), 1),
                };
        }

        private void StartOfDayLoad()
        {
            this.vendingMachine = new VendingMachine.Core.VendingMachineApplication();
            this.vendingMachine.Load(
                new MoneyHopper(Currency.GBP)
                    {
                        new StackOfCoins(new Coin(Currency.GBP, 0.01m), 100),
                        new StackOfCoins(new Coin(Currency.GBP, 0.02m), 100),
                        new StackOfCoins(new Coin(Currency.GBP, 0.05m), 50),
                        new StackOfCoins(new Coin(Currency.GBP, 0.10m), 50),
                        new StackOfCoins(new Coin(Currency.GBP, 0.20m), 50),
                        new StackOfCoins(new Coin(Currency.GBP, 0.50m), 50),
                        new StackOfCoins(new Coin(Currency.GBP, 1.00m), 50),
                        new StackOfCoins(new Coin(Currency.GBP, 2.00m), 50),
                    },
                new List<InventoryItem>()
                    {
                        new InventoryItem(new Product("Coke - Regular", 0.80m), 10, "A1"),
                        new InventoryItem(new Product("Coke - Diet", 0.80m), 10, "A2"),
                        new InventoryItem(new Product("Coke - Zere", 0.80m), 10, "A3"),
                        new InventoryItem(new Product("Fanta", 0.80m), 10, "A4"),
                        new InventoryItem(new Product("Sprite", 0.80m), 10, "A5"),
                    });
        }
    }
}
