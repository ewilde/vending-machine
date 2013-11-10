namespace VendingMachine.Core
{
    public interface ICurrencyValidator
    {
        bool SupportedCurrency(Currency currency);
    }
}