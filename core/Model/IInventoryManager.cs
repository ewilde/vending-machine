namespace VendingMachine.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Responsible for managing the inventory
    /// </summary>
    public interface IInventoryManager
    {
        void UpdateInventory(Product product);

        IEnumerable<InventoryItem> Inventory { get; }

        InventoryItem GetInventory(Product product);

        InventoryItem GetInventory(string location);

        void Load(IEnumerable<InventoryItem> inventoryItems);
    }
}