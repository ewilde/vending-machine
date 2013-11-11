// -----------------------------------------------------------------------
// <copyright file="CoinStackSpecs.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace VendingMachine.Core.Tests
{
    [Subject(typeof(CoinStack))]
    public class When_calculating_the_total_value_of_a_stack_of_coins : Util.WithResult<decimal>
    {
        static CoinStack Subject;

        Establish context = () => Subject = new CoinStack(new Coin(Currency.GBP, 0.02m), 51);

        Because of = () => Result = Subject.Total;

        It should_equal_the_number_of_coins_times_by_the_coin_denomiation = () => 
            Result.ShouldEqual(Subject.Coin.Denomination * Subject.Amount);
    }

    public class When_removing_a_coin_from_a_stack_of_coins : Util.WithResult<decimal>
    {
        const int OriginalAmount = 51;

        static CoinStack Subject;

        Establish context = () => Subject = new CoinStack(new Coin(Currency.GBP, 0.02m), OriginalAmount);

        Because of = () => Result = Subject.Remove();

        It should_decrement_the_amount_of_coins_in_the_stack = () => 
            Subject.Amount.ShouldEqual(OriginalAmount - 1);
    }

    public class When_adding_a_coin_to_a_stack_of_coins : Util.WithResult<decimal>
    {
        const int OriginalAmount = 51;

        static CoinStack Subject;

        Establish context = () => Subject = new CoinStack(new Coin(Currency.GBP, 0.02m), OriginalAmount);

        Because of = () => Result = Subject.Add();

        It should_increment_the_amount_of_coins_in_the_stack = () => 
            Subject.Amount.ShouldEqual(OriginalAmount + 1);
    }
}