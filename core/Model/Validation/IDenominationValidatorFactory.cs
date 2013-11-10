// -----------------------------------------------------------------------
// <copyright file="IDenominationValidatorFactory.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    public interface IDenominationValidatorFactory
    {
        IDenominationValidator Create(Currency currency);
    }
}