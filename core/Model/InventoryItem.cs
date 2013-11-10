// -----------------------------------------------------------------------
// <copyright file="InventoryItem.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    public class InventoryItem
    {
        private Product product;
        private int quantity;

        public InventoryItem(Product product, int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }         
    }
}