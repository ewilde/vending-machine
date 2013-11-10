namespace VendingMachine.Core
{
    using System.Collections.Generic;

    public interface IDenominationValidator
    {
        bool Validate(decimal amount);

        Currency UnderlyingCurrency { get; }

        HashSet<decimal> Denominations { get; }
    }
}