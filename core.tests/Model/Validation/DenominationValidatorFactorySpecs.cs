// -----------------------------------------------------------------------
// <copyright file="DenominationValidatorFactorySpecs.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace VendingMachine.Core.Tests
{
    [Subject(typeof(DenominationValidatorFactory))]
    public class When_creating_denomiation_validator_based_on_usd_currency : Util.WithSubjectAndResult<DenominationValidatorFactory, IDenominationValidator>
    {
        Because of = () => Result = Subject.Create(Currency.USD);

        It should_return_the_correct_validator_for_that_currency = () => 
            Result.UnderlyingCurrency.ShouldEqual(Currency.USD);

        It should_contain_the_list_of_us_coin_denominations = () =>
            Result.Denominations.ShouldContainOnly(new[] { 0.01m, 0.05m, 0.10m, 0.25m, 0.50m, 1.00m });
    }

    [Subject(typeof(DenominationValidatorFactory))]
    public class When_creating_denomiation_validator_based_on_gbp_currency : Util.WithSubjectAndResult<DenominationValidatorFactory, IDenominationValidator>
    {
        Because of = () => Result = Subject.Create(Currency.GBP);

        It should_return_the_correct_validator_for_that_currency = () => 
            Result.UnderlyingCurrency.ShouldEqual(Currency.GBP);

        It should_contain_the_list_of_british_coin_denominations = () =>
            Result.Denominations.ShouldContainOnly(new[] { 0.01m, 0.02m, 0.05m, 0.10m, 0.20m, 0.50m, 1.00m, 2.00m });
    }
}