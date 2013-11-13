// -----------------------------------------------------------------------
// <copyright file="Currency.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    /// <summary>
    /// Currencies that are supported by the system,
    /// used by <see cref="Coin"/>, <see cref="StackOfCoins"/> and <see cref="MoneyHopper"/>.
    /// </summary>
    public enum Currency
    {
        Unknown,
        GBP,
        USD
    }
}