// -----------------------------------------------------------------------
// <copyright file="CoinValidator.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    using System;

    /// <summary>
    /// Responsible for validating a coin structure. It enforces supported currency 
    /// and correct denomination for that currency
    /// </summary>
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
            if (!denominationValidator.Validate(coin.Denomination))
            {
                throw new ArgumentOutOfRangeException(string.Format("Denomination {0} is not supported for currency {1}.", coin.Denomination, coin.Currency));
            }
        }
    }
}