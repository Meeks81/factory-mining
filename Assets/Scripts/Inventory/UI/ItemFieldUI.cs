using TMPro;
using UnityEngine;

namespace InventorySystem.UI
{

    public class ItemFieldUI : MonoBehaviour
    {

        [SerializeField] private TMP_Text m_itemNameText;
        [SerializeField] private TMP_Text m_itemCountText;

        public void SetItem(ItemData itemData)
        {
            m_itemNameText.text = ItemsGlobalList.GetItem(itemData.ID).Name;
            m_itemCountText.text = itemData.count.ToString();
        }

    }

}