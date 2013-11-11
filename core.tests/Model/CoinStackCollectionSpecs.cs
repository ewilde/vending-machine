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


    [Subject(typeof(CoinStackCollection), "Validation")]
    public class when_adding_stacks_of_coins_to_the_collection : Util.WithSubject<CoinStackCollection>
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


    [Subject(typeof(CoinStackCollection), "Validation")]
    public class when_adding_mixed_coins_to_the_collection : Util.WithSubject<CoinStackCollection>
    {
        static CoinStack britishCoinStack;
        static CoinStack americanCoinStack;

        Establish context = () =>
            {
                Configure(x => x.For<Currency>().Use(Currency.USD)); // construct coin collection as USD collection
                americanCoinStack = new CoinStack(new Coin(Currency.USD, 0.25m), 1);
                britishCoinStack = new CoinStack(new Coin(Currency.GBP, 0.50m), 1);

                Subject.Add(americanCoinStack);
            };

        Because of = () => Exception = Catch.Exception(() => Subject.Add(britishCoinStack));

        It should_throw_an_exception = () => Exception.ShouldNotBeNull();

        It should_throw_an_argument_out_of_range_exception = () => Exception.ShouldBeOfType<ArgumentOutOfRangeException>();
    }

    [Subject(typeof(CoinStackCollection), "Adding")]
    public class when_adding_a_single_coin_to_the_collection : Util.WithSubject<CoinStackCollection>
    {
        static Coin onePennyCoin = new Coin(Currency.GBP, 0.01m);
        static Coin twoPennyCoin = new Coin(Currency.GBP, 0.02m);

        Establish context = () =>
            {
                Configure(x => x.For<Currency>().Use(Currency.GBP));
                Configure(x => x.For <IList<CoinStack>>().Use(new List<CoinStack>()));
                Subject.Add(new CoinStack(onePennyCoin, 0));
                Subject.Add(new CoinStack(twoPennyCoin, 0));
            };

        Because of = () => Subject.Add(onePennyCoin);

        It should_add_the_coin_to_the_correct_stack_based_on_its_denomination = () =>
            Subject[onePennyCoin].Amount.ShouldEqual(1);        
    }

    [Subject(typeof(CoinStackCollection), "Adding")]
    public class when_adding_a_stack_to_the_collection_that_already_contains_a_stack_for_that_denomiation : Util.WithSubject<CoinStackCollection>
    {
        static Coin onePennyCoin = new Coin(Currency.GBP, 0.01m);
        static Coin twoPennyCoin = new Coin(Currency.GBP, 0.02m);

        Establish context = () =>
            {
                Configure(x => x.For<Currency>().Use(Currency.GBP));
                Configure(x => x.For <IList<CoinStack>>().Use(new List<CoinStack>()));
                Subject.Add(new CoinStack(onePennyCoin, 2));
                Subject.Add(new CoinStack(twoPennyCoin, 0));
            };

        Because of = () => Subject.Add(new CoinStack(onePennyCoin, 3));

        It should_not_add_a_duplicate_stack = () =>
            Subject.Count.ShouldEqual(2);

        It should_combine_the_totals_of_the_two_stacks = () =>
            Subject[onePennyCoin].Amount.ShouldEqual(5);
    }

    [Subject(typeof(CoinStackCollection), "Lookup")]
    public class when_looking_up_an_item_by_using_a_valid_index : Util.WithSubjectAndResult<CoinStackCollection, CoinStack>
    {
        static Coin onePennyCoin = new Coin(Currency.GBP, 0.01m);
        static Coin twoPennyCoin = new Coin(Currency.GBP, 0.02m);
        static CoinStack onePennyStack = new CoinStack(onePennyCoin, 0);
        static CoinStack twoPennyStack = new CoinStack(twoPennyCoin, 0);

        Establish context = () =>
        {
            Configure(x => x.For<Currency>().Use(Currency.GBP));
            Configure(x => x.For<IList<CoinStack>>().Use(new List<CoinStack>()));
            Subject.Add(onePennyStack);
            Subject.Add(twoPennyStack);
        };

        Because of = () => Result = Subject[twoPennyCoin];

        It should_retrieve_the_item_from_the_collection = () => Result.ShouldEqual(twoPennyStack);
    }

    [Subject(typeof(CoinStackCollection), "Lookup")]
    public class when_looking_up_an_item_by_using_a_invalid_index : Util.WithSubjectAndResult<CoinStackCollection, CoinStack>
    {
        static Coin onePennyCoin = new Coin(Currency.GBP, 0.01m);
        static Coin twoPennyCoin = new Coin(Currency.GBP, 0.02m);
        static CoinStack onePennyStack = new CoinStack(onePennyCoin, 0);
        static CoinStack twoPennyStack = new CoinStack(twoPennyCoin, 0);

        Establish context = () =>
        {
            Configure(x => x.For<Currency>().Use(Currency.GBP));
            Configure(x => x.For<IList<CoinStack>>().Use(new List<CoinStack>()));
            Subject.Add(onePennyStack);
        };

        Because of = () => Exception = Catch.Exception(() => Result = Subject[twoPennyCoin]);

        It should_throw_an_exception = () => Exception.ShouldNotBeNull();

        It should_throw_an_index_out_of_range_exception = () => Exception.ShouldBeOfType<IndexOutOfRangeException>();
    }
}