// -----------------------------------------------------------------------
// <copyright file="VendingValidationSpec.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace VendingMachine.Core.Tests
{
    using System.Collections.Generic;

    [Subject(typeof(VendingValidation))]
    public class When_validating_sufficient_funds_with_less_money_than_the_product_price : Util.WithSubject<VendingValidation>
    {
        private Because of =
            () => Exception = Catch.Exception(()=>
            Subject.EnsureSufficientCoinsGiven(
                DummyInventory.Coke,
                new MoneyHopper(
                Currency.GBP, new List<StackOfCoins> { new StackOfCoins(new Coin(Currency.GBP, 0.05m), 10) })));

        It should_throw_an_exception = () => Exception.ShouldNotBeNull();

        It should_throw_an_exception_of_type_insufficient_funds_exception = () => Exception.ShouldBeOfType<InsufficientFundsException>();
    }

    [Subject(typeof(VendingValidation))]
    public class When_validating_sufficient_funds_with_exact_money_for_the_product_price : Util.WithSubject<VendingValidation>
    {
        private Because of =
            () =>
            Exception =
            Catch.Exception(
                () =>
                Subject.EnsureSufficientCoinsGiven(
                    DummyInventory.Coke,
                    new MoneyHopper(
                    Currency.GBP,
                    new List<StackOfCoins>
                        {
                            new StackOfCoins(new Coin(Currency.GBP, 0.10m), 3),
                            new StackOfCoins(new Coin(Currency.GBP, 0.50m), 1)
                        })));

        It should_not_throw_and_exception = () => Exception.ShouldBeNull();
    }
}