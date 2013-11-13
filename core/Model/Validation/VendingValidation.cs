// -----------------------------------------------------------------------
// <copyright file="VendingValidation.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    /// <summary>
    /// Performs all validation functions required to successfully process a vending request.
    /// </summary>
    public class VendingValidation : IVendingValidation
    {
        public void EnsureProductIsInStock(InventoryItem inventoryItem)
        {
            if (inventoryItem.Quantity <= 0)
            {
                throw new OutOfStockException(string.Format("Product [{0}] out of stock.", inventoryItem.Product), inventoryItem.Product);
            }
        }

        public void EnsureSufficientCoinsGiven(Product product, MoneyHopper coinsOffered)
        {
            if (coinsOffered.Total < product.Price)
            {
                throw new InsufficientFundsException(
                    string.Format("Insufficient funds for {0}.", product.Name), product.Price, coinsOffered.Total);
            }
        } 
    }
}