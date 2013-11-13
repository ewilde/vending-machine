// -----------------------------------------------------------------------
// <copyright file="InventoryItem.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    /// <summary>
    /// Responsible for tracking the amount of a given <see cref="Product"/> in stock.
    /// </summary>
    public class InventoryItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public string MachineLocation { get; set; }

        public InventoryItem(Product product, int quantity, string machineLocation)
        {
            Product = product;
            Quantity = quantity;
            MachineLocation = machineLocation;
        }
    }
}