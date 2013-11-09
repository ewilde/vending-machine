// -----------------------------------------------------------------------
// <copyright file="CurrencyValidatorSpecs.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace VendingMachine.Core.Tests
{
    [Subject(typeof(CurrencyValidator), "Validation")]
    public class When_calling_supported_currency_with_an_unsupported_currency : Util.WithSubjectAndResult<CurrencyValidator, bool>
    {
        Because of = () => Result = Subject.SupportedCurrency((Currency) 666);

        It should_return_false = () => Result.ShouldBeFalse();
    }

    [Subject(typeof(CurrencyValidator), "Validation")]
    public class When_calling_supported_currency_with_an_supported_currency : Util.WithSubjectAndResult<CurrencyValidator, bool>
    {
        Because of = () => Result = Subject.SupportedCurrency(Currency.GBP);

        It should_return_true = () => Result.ShouldBeTrue();
    }
}