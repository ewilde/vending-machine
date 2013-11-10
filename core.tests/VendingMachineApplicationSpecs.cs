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
                    new Coin(Currency.GBP, 0.05m), new Coin(Currency.GBP, 0.10m)
                },
            new HashSet<InventoryItem>
                {
                    new InventoryItem(product: new Product(name: "Coke", price: 0.80m), quantity: 10),
                    new InventoryItem(product: new Product(name: "Sprite", price: 0.60m), quantity: 5)
                });

        It should_report_the_amount_of_total_of_money = () => Subject.TotalMoneyAvailable.ShouldEqual(0.15m);

        It should_report_list_products_available = () => Subject.Inventory.Count().ShouldEqual(2);
    }

    public class when_purchasing_an_item_using_coins_higher_than_the_item_price : WithSubjectAndResult<VendingMachineApplication, CoinCollection>
    {
        
    }

    public class FullyLoadedVendingMachineContext : ContextBase
    {
        OnEstablish context = engine =>
            {
                FakeAccessor = engine;
                ContextBase.Subject<VendingMachineApplication>().Load();
            };

        public CoinCollection DefaultAmountOfCash()
        {
            CoinCollection coins = new CoinCollection(Currency.GBP);

            for (int i = 0; i < 100; i++)
            {
                coins.Add(new Coin());
            }
        }
    }
}