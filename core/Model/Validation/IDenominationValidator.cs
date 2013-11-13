namespace VendingMachine.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Validates that the denominations for a given coin are valid.
    /// For example $0.20 is not a valid coin.
    /// </summary>
    public interface IDenominationValidator
    {
        bool Validate(decimal amount);

        Currency UnderlyingCurrency { get; }

        HashSet<decimal> Denominations { get; }
    }
}