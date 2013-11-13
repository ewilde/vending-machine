// -----------------------------------------------------------------------
// <copyright file="InventoryManager.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Responsible for managing the inventory
    /// </summary>
    public class InventoryManager : IInventoryManager
    {
        private readonly List<InventoryItem> inventory;

        public InventoryManager()
        {
            inventory = new List<InventoryItem>();
        }

        public void UpdateInventory(Product product)
        {
            this.GetInventory(product).Quantity -= 1;
        }


        public IEnumerable<InventoryItem> Inventory
        {
            get
            {
                return this.inventory;
            }
        }

        public InventoryItem GetInventory(Product product)
        {
            return this.Inventory.First(item => item.Product.Equals(product));
        }

        public InventoryItem GetInventory(string location)
        {
            return this.Inventory.First(item => item.MachineLocation.Equals(location));
        }

        public void Load(IEnumerable<InventoryItem> inventoryItems)
        {
            this.inventory.AddRange(inventoryItems);
        }
    }
}