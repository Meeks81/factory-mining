using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem
{

    public class Inventory : MonoBehaviour
    {

        public UnityEvent OnItemsListUpdated { get; private set; } = new UnityEvent();
        public ItemData[] Items => _items.ToArray();

        private List<ItemData> _items = new List<ItemData>();

        /// <summary>Количество предмета в инвентаре по его ID/Названию</summary>
        /// <param name="itemID">ID/Название искомого предмета</param>
        /// <returns>Количество предмета в инвентаре</returns>
        public int GetItemCount(string itemID)
        {
            int itemIndex = GetItemIndex(itemID);
            if (itemIndex == -1)
                return 0;
            else
                return _items[itemIndex].count;
        }

        /// <summary>Поиск индекса предмета в массиве предметов инвентаря по его ID/Названию</summary>
        /// <param name="itemID">ID/Название искомого предмета</param>
        /// <returns>Индекс предмета в массиве предметов инвентаря</returns>
        public int GetItemIndex(string itemID) => _items.FindIndex(item => item.ID == itemID);

        /// <summary>Добавление нескольких предметов в инвентарь</summary>
        /// <param name="itemID">ID/Название предмета</param>
        /// <param name="count">Количество предметов</param>
        public void AddItem(string itemID, int count)
        {
            if (count <= 0)
                return;

            int itemIndex = GetItemIndex(itemID);

            if (itemIndex == -1)
            {
                _items.Add(new ItemData()
                {
                    ID = itemID,
                    count = count
                });
            }
            else
            {
                _items[itemIndex] = new ItemData()
                {
                    ID = itemID,
                    count = _items[itemIndex].count + count
                };
            }
            OnItemsListUpdated?.Invoke();
        }

        /// <summary>Удаляет несколько предметов в инвентаре</summary>
        /// <param name="itemID">ID/Название предмета</param>
        /// <param name="count">Количество предметов</param>
        /// <returns>False - если предмет не найден или в инвентаре не достаточное количество предмета с данным ID/Названием</returns>
        public bool TakeItem(string itemID, int count)
        {
            if (count <= 0)
                return false;

            int itemIndex = GetItemIndex(itemID);

            if (itemIndex == -1 || _items[itemIndex].count < count)
                return false;

            if (_items[itemIndex].count == count)
            {
                RemoveItem(itemID);
            }
            else
            {
                _items[itemIndex] = new ItemData()
                {
                    ID = itemID,
                    count = _items[itemIndex].count - count
                };
            }
            OnItemsListUpdated?.Invoke();
            return true;
        }

        /// <summary>Удаляет из инвентаря предметы с передаваемым ID/Названием</summary>
        /// <param name="itemID">ID/Название удаляемого предмета</param>
        public void RemoveItem(string itemID)
        {
            int itemIndex = GetItemIndex(itemID);
            _items.RemoveAt(itemIndex);
            OnItemsListUpdated?.Invoke();
        }

    }

}