namespace VendingMachine.Core
{
    public interface IDenominationValidator
    {
        bool Validate(Currency currency, decimal amount);
    }
}