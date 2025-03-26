using FloatingUtils;
using InventorySystem;
using TMPro;
using UnityEngine;

public class FloatingFarmingItemsHud : FloatingObject
{

    [SerializeField] private TMP_Text m_itemNameText;
    [SerializeField] private TMP_Text m_itemCountText;
    [SerializeField] private FarmingBuilding m_farmingBuilding;

    public FarmingBuilding farmingBuilding
    {
        get => m_farmingBuilding;
        set
        {
            // Отписка от события старого здания
            if (m_farmingBuilding)
                m_farmingBuilding.OnItemsCountChanged.RemoveListener(UpdateItemsCount);

            m_farmingBuilding = value;
            ConnectObject = value.transform;
            UpdateItemNameText();

            // Не обновляет худ, если тот не активен. Обновление происходит при активации объекта
            if (gameObject.activeInHierarchy)
            {
                // Подписка на событие нового здания
                if (m_farmingBuilding)
                    m_farmingBuilding.OnItemsCountChanged.AddListener(UpdateItemsCount);
                UpdateItemsCount(m_farmingBuilding.CurrentItemsCount);
            }
        }
    }

    private void OnEnable()
    {
        if (m_farmingBuilding)
        {
            UpdateItemNameText();
            UpdateItemsCount(m_farmingBuilding.CurrentItemsCount);
            m_farmingBuilding.OnItemsCountChanged.AddListener(UpdateItemsCount);
        }
    }

    private void OnDisable()
    {
        if (m_farmingBuilding)
            m_farmingBuilding.OnItemsCountChanged.RemoveListener(UpdateItemsCount);
    }

    private void UpdateItemNameText()
    {
        if (m_farmingBuilding)
            m_itemNameText.text = ItemsGlobalList.GetItem(m_farmingBuilding.FarmItemID).Name;
    }

    private void UpdateItemsCount(int count)
    {
        m_itemCountText.text = count.ToString();
    }

}
