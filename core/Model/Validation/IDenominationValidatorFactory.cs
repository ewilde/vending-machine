// -----------------------------------------------------------------------
// <copyright file="IDenominationValidatorFactory.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    /// <summary>
    /// Creates the appropriate validator for a given <see cref="Currency"/>.
    /// </summary>
    public interface IDenominationValidatorFactory
    {
        IDenominationValidator Create(Currency currency);
    }
}