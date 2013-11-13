namespace VendingMachine.Core
{
    /// <summary>
    /// Validates supported currencies for the system.
    /// </summary>
    public interface ICurrencyValidator
    {
        bool SupportedCurrency(Currency currency);
    }
}