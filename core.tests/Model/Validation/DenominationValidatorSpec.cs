// -----------------------------------------------------------------------
// <copyright file="DenominationValidatorSpec.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace VendingMachine.Core.Tests
{
    [Subject(typeof(DenominationValidator))]
    public class When_validating_a_coin_with_a_valid_denomiation_for_a_currency : Util.WithResult<bool>
    {
        static DenominationValidator Validator;
        Establish context = () => Validator = (DenominationValidator)new DenominationValidatorFactory().Create(Currency.GBP);

        Because of = () => Result = Validator.Validate(0.05m);

        It should_return_true = () => Result.ShouldBeTrue();
    }

    [Subject(typeof(DenominationValidator))]
    public class When_validating_a_coin_with_an_invalid_denomiation_for_a_currency : Util.WithResult<bool>
    {
        static DenominationValidator Validator;
        Establish context = () => Validator = (DenominationValidator)new DenominationValidatorFactory().Create(Currency.GBP);

        Because of = () => Result = Validator.Validate(0.25m);

        It should_return_false = () => Result.ShouldBeFalse();
    }
}