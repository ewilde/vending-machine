﻿// -----------------------------------------------------------------------
// <copyright file="CurrencyValidator.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    /// <summary>
    /// Validates supported currencies for the system.
    /// </summary>
    public class CurrencyValidator : ICurrencyValidator
    {
        public bool SupportedCurrency(Currency currency)
        {
            return currency >= Currency.GBP && currency <= Currency.USD;
        }
    }
}