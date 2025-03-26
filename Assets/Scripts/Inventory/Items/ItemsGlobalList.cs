using System.Collections.Generic;

namespace InventorySystem
{

    // Глобальный лист описания предметов инвентаря
    public static class ItemsGlobalList
    {

        private static Dictionary<string, ItemContent> _items = new Dictionary<string, ItemContent>()
        {
            { "1", new ItemContent() { Name = "Flour" } },
            { "2", new ItemContent() { Name = "Iron" } },
            { "3", new ItemContent() { Name = "Wheat" } },
            { "4", new ItemContent() { Name = "Fuel" } },
            { "5", new ItemContent() { Name = "Eggs" } },
        };

        public static ItemContent GetItem(string itemID)
        {
            if (_items.ContainsKey(itemID))
                return _items[itemID];
            else
                return default;
        }

        public static bool Contains(string itemID) => _items.ContainsKey(itemID);

    }

}