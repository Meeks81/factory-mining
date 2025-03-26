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

        /// <summary>���������� �������� � ��������� �� ��� ID/��������</summary>
        /// <param name="itemID">ID/�������� �������� ��������</param>
        /// <returns>���������� �������� � ���������</returns>
        public int GetItemCount(string itemID)
        {
            int itemIndex = GetItemIndex(itemID);
            if (itemIndex == -1)
                return 0;
            else
                return _items[itemIndex].count;
        }

        /// <summary>����� ������� �������� � ������� ��������� ��������� �� ��� ID/��������</summary>
        /// <param name="itemID">ID/�������� �������� ��������</param>
        /// <returns>������ �������� � ������� ��������� ���������</returns>
        public int GetItemIndex(string itemID) => _items.FindIndex(item => item.ID == itemID);

        /// <summary>���������� ���������� ��������� � ���������</summary>
        /// <param name="itemID">ID/�������� ��������</param>
        /// <param name="count">���������� ���������</param>
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

        /// <summary>������� ��������� ��������� � ���������</summary>
        /// <param name="itemID">ID/�������� ��������</param>
        /// <param name="count">���������� ���������</param>
        /// <returns>False - ���� ������� �� ������ ��� � ��������� �� ����������� ���������� �������� � ������ ID/���������</returns>
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

        /// <summary>������� �� ��������� �������� � ������������ ID/���������</summary>
        /// <param name="itemID">ID/�������� ���������� ��������</param>
        public void RemoveItem(string itemID)
        {
            int itemIndex = GetItemIndex(itemID);
            _items.RemoveAt(itemIndex);
            OnItemsListUpdated?.Invoke();
        }

    }

}