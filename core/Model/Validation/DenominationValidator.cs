// -----------------------------------------------------------------------
// <copyright file="DenominationValidator.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Validates that the denominations for a given coin are valid.
    /// For example $0.20 is not a valid coin.
    /// </summary>
    public class DenominationValidator : IDenominationValidator
    {
        public DenominationValidator(Currency currency, IEnumerable<decimal> denominations)
        {
            this.UnderlyingCurrency = currency;
            this.Denominations = new HashSet<decimal>(denominations);
        }

        public HashSet<decimal> Denominations { get; private set; }

        public Currency UnderlyingCurrency { get; private set; }

        public bool Validate(decimal amount)
        {
            return this.Denominations.Contains(amount);
        }
    }
}