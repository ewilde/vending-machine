namespace VendingMachine.Core
{
    /// <summary>
    /// Performs all validation functions required to successfully process a vending request.
    /// </summary>
    public interface IVendingValidation
    {
        void EnsureProductIsInStock(InventoryItem inventoryItem);

        void EnsureSufficientCoinsGiven(Product product, MoneyHopper coinsOffered);
    }
}