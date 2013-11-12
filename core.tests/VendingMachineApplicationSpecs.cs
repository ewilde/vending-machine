// -----------------------------------------------------------------------
// <copyright file="VendingMachineApplication.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Machine.Fakes;
    using Machine.Specifications;

    using VendingMachine.Core.Tests.Util;

    [Subject(typeof(VendingMachineApplication), "initialization")]
    public class when_a_machine_is_loaded_at_the_beginning_off_the_day : Util.WithSubject<VendingMachineApplication>
    {
        Establish context = () => {  };

        Because of = () => Subject.Load(
            new MoneyHopper(Currency.GBP)
                {
                    new StackOfCoins(new Coin(Currency.GBP, 0.01m), 15), 
                    new StackOfCoins(new Coin(Currency.GBP, 0.05m), 10), 
                    new StackOfCoins(new Coin(Currency.GBP, 0.10m), 5)
                },
            new List<InventoryItem>
                {
                    new InventoryItem(product: new Product(name: "Coke", price: 0.80m), quantity: 10, machineLocation: "A1"),
                    new InventoryItem(product: new Product(name: "Sprite", price: 0.60m), quantity: 5, machineLocation: "A2")
                });

        It should_report_the_total_amount_of_money = () => Subject.TotalMoneyAvailable.ShouldEqual(1.15m);

        It should_store_the_products_available = () => Subject.Inventory.Count().ShouldEqual(2);
    }

    [Subject(typeof(VendingMachineApplication), "purchasing")]
    public class when_purchasing_an_item_using_a_single_coin : WithSubjectAndResult<VendingMachineApplication, MoneyHopper>
    {
        static decimal originalBalance;
        static int originalCokeQuantity;
        static CoinPurse customersCoins;

        Establish context = () =>
            {
                With<FullyLoadedVendingMachineContext>();
                originalBalance = Subject.TotalMoneyAvailable;
                originalCokeQuantity = Subject.Inventory.First(item => item.Product.Equals(DummyInventory.Coke)).Quantity;
                customersCoins = new CoinPurse(Currency.GBP)
                    {
                        new StackOfCoins(new Coin(Currency.GBP, 2.00m), 1)
                    };
            };

        Because of = () => Result = Subject.Purchase(DummyInventory.Coke, customersCoins);

        It should_return_the_correct_amount_of_change = () => Result.Total.ShouldEqual(1.20m);

        It should_item_cost_should_be_credited_to_the_vending_machine_balane = () =>
            Subject.TotalMoneyAvailable.ShouldEqual(originalBalance + DummyInventory.Coke.Price);

        It should_return_change_made_up_of_the_highest_value_coins = () =>
            {
                Result[0].Coin.Denomination.ShouldEqual(1.00m);
                Result[1].Coin.Denomination.ShouldEqual(0.20m);
            };

        It should_decrease_the_inventory_count_for_the_purchased_item_by_one = () =>
                Subject.Inventory.First(item => item.Product.Equals(DummyInventory.Coke))
                       .Quantity.ShouldEqual(originalCokeQuantity - 1);
            
    }

    [Subject(typeof(VendingMachineApplication), "purchasing")]
    public class when_purchasing_an_item_using_a_many_coins : WithSubjectAndResult<VendingMachineApplication, MoneyHopper>
    {
        static decimal originalBalance;
        static CoinPurse customersCoins;

        Establish context = () =>
            {
                With<FullyLoadedVendingMachineContext>();
                originalBalance = Subject.TotalMoneyAvailable;
                customersCoins = new CoinPurse(Currency.GBP)
                    {
                        new StackOfCoins(new Coin(Currency.GBP, 0.20m), 2),
                        new StackOfCoins(new Coin(Currency.GBP, 0.10m), 3),
                        new StackOfCoins(new Coin(Currency.GBP, 0.05m), 4),
                    };
            };

        Because of = () => Result = Subject.Purchase(DummyInventory.Coke, customersCoins);

        It should_return_the_correct_amount_of_change = () => Result.Total.ShouldEqual(0.10m);

        It should_item_cost_should_be_credited_to_the_vending_machine_balane = () =>
            Subject.TotalMoneyAvailable.ShouldEqual(originalBalance + DummyInventory.Coke.Price);

        It should_return_change_made_up_of_the_highest_value_coins = () =>
            {
                Result[0].Coin.Denomination.ShouldEqual(0.10m);
            };
    }

    [Subject(typeof(VendingMachineApplication), "purchasing")]
    public class when_purchasing_an_item_using_coins_less_than_the_product_price : WithSubjectAndResult<VendingMachineApplication, MoneyHopper>
    {
        static CoinPurse customersCoins;
        static decimal originalBalance;
        
        Establish context = () =>
            {
                With<FullyLoadedVendingMachineContext>();
                originalBalance = Subject.TotalMoneyAvailable;
                customersCoins = new CoinPurse(Currency.GBP)
                    {
                        new StackOfCoins(new Coin(Currency.GBP, 0.50m), 1),
                        new StackOfCoins(new Coin(Currency.GBP, 0.20m), 1)
                    };
            };

        Because of = () => Exception =
            Catch.Exception(()=> Result = Subject.Purchase(DummyInventory.Coke, customersCoins));

        It should_throw_an_exception = () => Exception.ShouldNotBeNull();

        It should_throw_an_exception_of_type_insufficient_funds_exception = () => Exception.ShouldBeOfType<InsufficientFundsException>();

        It should_leave_the_vending_machine_cash_balance_unaltered = () => Subject.TotalMoneyAvailable.ShouldEqual(originalBalance);

        It should_not_decrease_the_inventory_count_for_the_attempted_purchase = () =>
            Subject.Inventory.First(item => item.Product.Equals(DummyInventory.Coke)).Quantity.ShouldEqual(10);
    }

    [Subject(typeof(VendingMachineApplication), "purchasing")]
    public class when_purchasing_an_item_and_the_machine_cannot_vend_exact_change : WithSubjectAndResult<VendingMachineApplication, MoneyHopper>
    {
        static CoinPurse customersCoins;
        static decimal originalBalance;
        
        Establish context = () =>
            {
                With(new FullyLoadedVendingMachineContext(
                    new MoneyHopper(Currency.GBP, new List<StackOfCoins>(new[] { new StackOfCoins(new Coin(Currency.GBP, 2.0m), 100) }))));

                originalBalance = Subject.TotalMoneyAvailable;
                customersCoins = new CoinPurse(Currency.GBP)
                {
                    new StackOfCoins(new Coin(Currency.GBP, 0.50m), 1),
                    new StackOfCoins(new Coin(Currency.GBP, 0.20m), 2)
                };
            };

        Because of = () => Exception = Catch.Exception(() => Result = Subject.Purchase(DummyInventory.Coke, // 80p 
            customersCoins));

        It should_return_the_change = () => customersCoins.Total.ShouldEqual(0.90m);

        It should_throw_an_exception = () => Exception.ShouldNotBeNull();

        It should_leave_the_vending_machine_cash_balance_unaltered = () => Subject.TotalMoneyAvailable.ShouldEqual(originalBalance);
    }

    [Subject(typeof(VendingMachineApplication), "Validation")]
    public class when_attempting_to_purchase_an_out_of_stock_item
    {
        Establish context = () => {  };

        Because of = () => { };

        It should_throw_an_exception = () => { };

        It should_throw_an_exception_of_type_product_out_of_stock_exception = () => { asd };
    }

    public class FullyLoadedVendingMachineContext : ContextBase
    {
        private static MoneyHopper cash;

        OnEstablish context = engine =>
            {
                FakeAccessor = engine;
                ContextBase.Subject<VendingMachineApplication>().Load(cash, Inventory());
            };

        public FullyLoadedVendingMachineContext()
        {
            cash = Cash();
        }

        public FullyLoadedVendingMachineContext(MoneyHopper moneyHopper)
        {
            cash = moneyHopper;
        }

        public static MoneyHopper Cash()
        {
            return new MoneyHopper(Currency.GBP)
                {
                    new StackOfCoins(new Coin(Currency.GBP, 0.01m), 100),
                    new StackOfCoins(new Coin(Currency.GBP, 0.02m), 100),
                    new StackOfCoins(new Coin(Currency.GBP, 0.05m), 50),
                    new StackOfCoins(new Coin(Currency.GBP, 0.10m), 50),
                    new StackOfCoins(new Coin(Currency.GBP, 0.20m), 50),
                    new StackOfCoins(new Coin(Currency.GBP, 0.50m), 50),
                    new StackOfCoins(new Coin(Currency.GBP, 1.00m), 50),
                    new StackOfCoins(new Coin(Currency.GBP, 2.00m), 50),
                };
        }

        public static List<InventoryItem> Inventory()
        {
            return new List<InventoryItem>
                {
                    new InventoryItem(product: DummyInventory.Coke, quantity: 10, machineLocation: "A1"),
                    new InventoryItem(product: DummyInventory.Sprite, quantity: 5, machineLocation: "A2")
                };
        }
    }

    public static class DummyInventory
    {
        public static readonly Product Coke = new Product(name: "Coke", price: 0.80m);

        public static readonly Product Sprite = new Product(name: "Sprite", price: 0.60m);
    }
}