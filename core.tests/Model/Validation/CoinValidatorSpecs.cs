// -----------------------------------------------------------------------
// <copyright file="CoinSpecs.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core.Tests
{
    using System;

    using Machine.Specifications;
    using Machine.Specifications.Model;

    using Machine.Fakes;
    using VendingMachine.Core.Tests.Util;

    [Subject(typeof(CoinValidator), "Validation")]
    public class when_validating_coins_of_a_supported_currency : Util.WithSubject<CoinValidator>
    {
        static Coin coin;
        
        Establish context = () =>
            { 
                coin = new Coin(Currency.GBP, 0.5m);
                With(new ValidCoinContext(coin));
            };
        
        Because of = () => Subject.Validate(coin);

        It should_verify_the_coins_currency_is_supported = () => The<ICurrencyValidator>().WasToldTo(call => call.SupportedCurrency(coin.Currency));        

        It should_verify_the_coins_denomiation_is_valid = () => The<IDenominationValidator>().WasToldTo(call => call.Validate(coin.Denomination));
    }          

    [Subject(typeof(CoinValidator), "Validation")]
    public class when_validating_coins_based_on_an_unsupported_currency : Util.WithSubject<CoinValidator>
    {
        static Coin coin;
        
        Establish context = () =>
            {
                coin = new Coin((Currency)666, 0.5m);
                With(new InvalidCoinContext(coin));
            };
        
        Because of = () => Exception = Catch.Exception(() => Subject.Validate(coin));

        It should_throw_an_exception = () => Exception.ShouldNotBeNull();
    }          

    public class ValidCoinContext
    {
        private static Coin coin;

        public ValidCoinContext(Coin coin)
        {
            ValidCoinContext.coin = coin;
        }

        private OnEstablish context =
            accessor =>
                {
                    accessor.The<ICurrencyValidator>().WhenToldTo(call => call.SupportedCurrency(Param<Currency>.IsAnything)).Return(true);                     
                    accessor.The<IDenominationValidatorFactory>()
                            .WhenToldTo(call => call.Create(coin.Currency))
                            .Return(accessor.The<IDenominationValidator>());
                };
    }   

    public class InvalidCoinContext
    {
        private static Coin coin;

        public InvalidCoinContext(Coin coin)
        {
            InvalidCoinContext.coin = coin;
        }

        private OnEstablish context =
            accessor =>
                {
                    accessor.The<ICurrencyValidator>().WhenToldTo(call => call.SupportedCurrency(Param<Currency>.IsAnything)).Return(false);                     
                    accessor.The<IDenominationValidatorFactory>()
                            .WhenToldTo(call => call.Create(coin.Currency))
                            .Return(accessor.The<IDenominationValidator>());
                };
    }
}