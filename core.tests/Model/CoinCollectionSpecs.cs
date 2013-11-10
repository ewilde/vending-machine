// -----------------------------------------------------------------------
// <copyright file="CoinCollectionTests.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core.Tests
{
    using System;

    using Machine.Fakes;
    using Machine.Specifications;


    [Subject(typeof(CoinCollection), "Validation")]
    public class when_adding_coins_to_the_collection : Util.WithSubject<CoinCollection>
    {
        static Coin coinAdding;
        Establish context = () =>
            {
                Configure(x => x.For<Currency>().Use(Currency.GBP)); // construct coin collection as GBP collection
                coinAdding = new Coin(Currency.GBP, 0.50m);
            };

        Because of = () => Subject.Add(new CoinStack(coinAdding, 10));

        It should_check_the_coin_is_valid_before_adding_to_the_collection = () => The<ICoinValidator>().WasToldTo(call => call.Validate(coinAdding));
    }


    [Subject(typeof(CoinCollection), "Validation")]
    public class when_adding_mixed_coins_to_the_collection : Util.WithSubject<CoinCollection>
    {
        static CoinStack britishCoin;
        static CoinStack americanCoin;

        Establish context = () =>
            {
                Configure(x => x.For<Currency>().Use(Currency.USD)); // construct coin collection as USD collection
                americanCoin = new CoinStack(new Coin(Currency.USD, 0.25m), 1);
                britishCoin = new CoinStack(new Coin(Currency.GBP, 0.50m), 1);

                Subject.Add(americanCoin);
            };

        Because of = () => Exception = Catch.Exception(() => Subject.Add(britishCoin));

        It should_throw_an_exception = () => Exception.ShouldNotBeNull();

        It should_throw_an_argument_out_of_range_exception = () => Exception.ShouldBeOfType<ArgumentOutOfRangeException>();
    }


}