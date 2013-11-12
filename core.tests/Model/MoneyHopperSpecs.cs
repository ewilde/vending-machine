// -----------------------------------------------------------------------
// <copyright file="CoinCollectionTests.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core.Tests
{
    using System;
    using System.Collections.Generic;

    using Machine.Fakes;
    using Machine.Specifications;


    [Subject(typeof(MoneyHopper), "Validation")]
    public class when_adding_stacks_of_coins_to_the_hopper : Util.WithSubject<MoneyHopper>
    {
        static Coin coinAdding;
        Establish context = () =>
            {
                Configure(x => x.For<Currency>().Use(Currency.GBP)); // construct coin collection as GBP collection
                Configure(x => x.For<IList<StackOfCoins>>().Use(new List<StackOfCoins>()));               
                coinAdding = new Coin(Currency.GBP, 0.50m);
            };

        Because of = () => Subject.Add(new StackOfCoins(coinAdding, 10));

        It should_check_the_coin_is_valid_before_adding_to_the_hopper = () => The<ICoinValidator>().WasToldTo(call => call.Validate(coinAdding));
    }


    [Subject(typeof(MoneyHopper), "Validation")]
    public class when_adding_mixed_coins_to_the_hopper : Util.WithSubject<MoneyHopper>
    {
        static StackOfCoins britishCoinStack;
        static StackOfCoins americanCoinStack;

        Establish context = () =>
            {
                Configure(x => x.For<Currency>().Use(Currency.USD)); // construct coin collection as USD collection
                Configure(x => x.For<IList<StackOfCoins>>().Use(new List<StackOfCoins>()));
                americanCoinStack = new StackOfCoins(new Coin(Currency.USD, 0.25m), 1);
                britishCoinStack = new StackOfCoins(new Coin(Currency.GBP, 0.50m), 1);

                Subject.Add(americanCoinStack);
            };

        Because of = () => Exception = Catch.Exception(() => Subject.Add(britishCoinStack));

        It should_throw_an_exception = () => Exception.ShouldNotBeNull();

        It should_throw_an_argument_out_of_range_exception = () => Exception.ShouldBeOfType<ArgumentOutOfRangeException>();
    }

    [Subject(typeof(MoneyHopper), "Adding")]
    public class when_adding_a_single_coin_to_the_hopper : Util.WithSubject<MoneyHopper>
    {
        static Coin onePennyCoin = new Coin(Currency.GBP, 0.01m);
        static Coin twoPennyCoin = new Coin(Currency.GBP, 0.02m);

        Establish context = () =>
            {
                Configure(x => x.For<Currency>().Use(Currency.GBP));
                Configure(x => x.For <IList<StackOfCoins>>().Use(new List<StackOfCoins>()));
                Subject.Add(new StackOfCoins(onePennyCoin, 0));
                Subject.Add(new StackOfCoins(twoPennyCoin, 0));
            };

        Because of = () => Subject.Add(onePennyCoin);

        It should_add_the_coin_to_the_correct_stack_based_on_its_denomination = () =>
            Subject[onePennyCoin].Amount.ShouldEqual(1);        
    }

    [Subject(typeof(MoneyHopper), "Adding")]
    public class when_adding_a_stack_to_the_hopper_that_already_contains_a_stack_for_that_denomiation : Util.WithSubject<MoneyHopper>
    {
        static Coin onePennyCoin = new Coin(Currency.GBP, 0.01m);
        static Coin twoPennyCoin = new Coin(Currency.GBP, 0.02m);

        Establish context = () =>
            {
                Configure(x => x.For<Currency>().Use(Currency.GBP));
                Configure(x => x.For <IList<StackOfCoins>>().Use(new List<StackOfCoins>()));
                Subject.Add(new StackOfCoins(onePennyCoin, 2));
                Subject.Add(new StackOfCoins(twoPennyCoin, 0));
            };

        Because of = () => Subject.Add(new StackOfCoins(onePennyCoin, 3));

        It should_not_add_a_duplicate_stack = () =>
            Subject.Count.ShouldEqual(2);

        It should_combine_the_totals_of_the_two_stacks = () =>
            Subject[onePennyCoin].Amount.ShouldEqual(5);
    }

    [Subject(typeof(MoneyHopper), "Lookup")]
    public class when_looking_up_an_item_by_using_a_valid_index : Util.WithSubjectAndResult<MoneyHopper, StackOfCoins>
    {
        static Coin onePennyCoin = new Coin(Currency.GBP, 0.01m);
        static Coin twoPennyCoin = new Coin(Currency.GBP, 0.02m);
        static StackOfCoins onePennyStack = new StackOfCoins(onePennyCoin, 0);
        static StackOfCoins twoPennyStack = new StackOfCoins(twoPennyCoin, 0);

        Establish context = () =>
        {
            Configure(x => x.For<Currency>().Use(Currency.GBP));
            Configure(x => x.For<IList<StackOfCoins>>().Use(new List<StackOfCoins>()));
            Subject.Add(onePennyStack);
            Subject.Add(twoPennyStack);
        };

        Because of = () => Result = Subject[twoPennyCoin];

        It should_retrieve_the_item_from_the_collection = () => Result.ShouldEqual(twoPennyStack);
    }

    [Subject(typeof(MoneyHopper), "Lookup")]
    public class when_looking_up_an_item_by_using_a_invalid_index : Util.WithSubjectAndResult<MoneyHopper, StackOfCoins>
    {
        static Coin onePennyCoin = new Coin(Currency.GBP, 0.01m);
        static Coin twoPennyCoin = new Coin(Currency.GBP, 0.02m);
        static StackOfCoins onePennyStack = new StackOfCoins(onePennyCoin, 0);
        static StackOfCoins twoPennyStack = new StackOfCoins(twoPennyCoin, 0);

        Establish context = () =>
        {
            Configure(x => x.For<Currency>().Use(Currency.GBP));
            Configure(x => x.For<IList<StackOfCoins>>().Use(new List<StackOfCoins>()));
            Subject.Add(onePennyStack);
        };

        Because of = () => Exception = Catch.Exception(() => Result = Subject[twoPennyCoin]);

        It should_throw_an_exception = () => Exception.ShouldNotBeNull();

        It should_throw_an_index_out_of_range_exception = () => Exception.ShouldBeOfType<IndexOutOfRangeException>();
    }
}