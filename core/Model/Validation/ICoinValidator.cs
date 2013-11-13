namespace VendingMachine.Core
{
    /// <summary>
    /// Responsible for validating a coin structure. It enforces supported currency 
    /// and correct denomination for that currency
    /// </summary>
    public interface ICoinValidator
    {
        void Validate(Coin coin);
    }
}