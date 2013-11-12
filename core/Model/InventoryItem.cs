// -----------------------------------------------------------------------
// <copyright file="InventoryItem.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    public class InventoryItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public InventoryItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}