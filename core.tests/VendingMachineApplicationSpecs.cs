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
            new CoinCollection(Currency.GBP)
                {
                    new CoinStack(new Coin(Currency.GBP, 0.01m), 15), 
                    new CoinStack(new Coin(Currency.GBP, 0.05m), 10), 
                    new CoinStack(new Coin(Currency.GBP, 0.10m), 5)
                },
            new HashSet<InventoryItem>
                {
                    new InventoryItem(product: new Product(name: "Coke", price: 0.80m), quantity: 10),
                    new InventoryItem(product: new Product(name: "Sprite", price: 0.60m), quantity: 5)
                });

        It should_report_the_amount_of_total_of_money = () => Subject.TotalMoneyAvailable.ShouldEqual(1.15m);

        It should_report_list_products_available = () => Subject.Inventory.Count().ShouldEqual(2);
    }

    [Subject(typeof(VendingMachineApplication), "purchasing")]
    public class when_purchasing_an_item_using_coins_higher_than_the_item_price : WithSubjectAndResult<VendingMachineApplication, CoinCollection>
    {
        Establish context = () => With<FullyLoadedVendingMachineContext>();

        Because of = () => Result = Subject.Purchase(DummyInventory.Coke, // 90p 
            new CoinCollection(Currency.GBP)
                {
                    new CoinStack(new Coin(Currency.GBP, 0.50m), 1),
                    new CoinStack(new Coin(Currency.GBP, 0.20m), 2)
                });

        It should_return_the_correct_amount_of_change = () => Result.Total.ShouldEqual(0.10m);
    }

    public class FullyLoadedVendingMachineContext : ContextBase
    {
        OnEstablish context = engine =>
            {
                FakeAccessor = engine;
                ContextBase.Subject<VendingMachineApplication>().Load(Cash(), Inventory());
            };

        
        public static CoinCollection Cash()
        {
            return new CoinCollection(Currency.GBP)
                {
                    new CoinStack(new Coin(Currency.GBP, 0.01m), 100),
                    new CoinStack(new Coin(Currency.GBP, 0.02m), 100),
                    new CoinStack(new Coin(Currency.GBP, 0.05m), 50),
                    new CoinStack(new Coin(Currency.GBP, 0.10m), 50),
                    new CoinStack(new Coin(Currency.GBP, 0.20m), 50),
                    new CoinStack(new Coin(Currency.GBP, 0.50m), 50),
                    new CoinStack(new Coin(Currency.GBP, 1.00m), 50),
                    new CoinStack(new Coin(Currency.GBP, 2.00m), 50),
                };
        }

        public static HashSet<InventoryItem> Inventory()
        {
            return new HashSet<InventoryItem>
                {
                    new InventoryItem(product: DummyInventory.Coke, quantity: 10),
                    new InventoryItem(product: DummyInventory.Sprite, quantity: 5)
                };
        }
    }

    public static class DummyInventory
    {
        public static readonly Product Coke = new Product(name: "Coke", price: 0.80m);

        public static readonly Product Sprite = new Product(name: "Sprite", price: 0.60m);
    }
}