// -----------------------------------------------------------------------
// <copyright file="DenominationValidatorFactory.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// Creates the appropriate validator for a given <see cref="Currency"/>.
    /// </summary>
    public class DenominationValidatorFactory : IDenominationValidatorFactory
    {
        private static readonly Dictionary<Currency, decimal[]> denominations;

        private readonly ConcurrentDictionary<Currency, IDenominationValidator> validators;
 
        static DenominationValidatorFactory()
        {
            denominations = new Dictionary<Currency, decimal[]>
                {
                    { Currency.GBP, new[] { 0.01m, 0.02m, 0.05m, 0.10m, 0.20m, 0.50m, 1.00m, 2.00m } },
                    { Currency.USD, new[] { 0.01m, 0.05m, 0.10m, 0.25m, 0.50m, 1.00m }}
                };
        }

        public DenominationValidatorFactory()
        {
            this.validators = new ConcurrentDictionary<Currency, IDenominationValidator>();
        }

        public IDenominationValidator Create(Currency currency)
        {
            return this.validators.GetOrAdd(currency, key => new DenominationValidator(currency, denominations[key]));            
        }
    }
}