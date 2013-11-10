// -----------------------------------------------------------------------
// <copyright file="CoinValidator.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    using System;

    public class CoinValidator : ICoinValidator
    {
        private readonly ICurrencyValidator currencyValidator;
        private readonly IDenominationValidatorFactory denominationValidatorFactory;

        public CoinValidator(ICurrencyValidator currencyValidator, IDenominationValidatorFactory denominationValidatorFactory)
        {
            this.currencyValidator = currencyValidator;
            this.denominationValidatorFactory = denominationValidatorFactory;
        }

        public void Validate(Coin coin)
        {
            if (!currencyValidator.SupportedCurrency(coin.Currency))
            {
                throw new ArgumentOutOfRangeException(string.Format("Currency {0} is not support.", coin.Currency));
            }

            var denominationValidator = this.denominationValidatorFactory.Create(coin.Currency);
            denominationValidator.Validate(coin.Denomination);
        }
    }
}