using UnityEngine;
using UnityEngine.Pool;

namespace InventorySystem.UI
{

    public class InventoryUI : MonoBehaviour
    {

        [SerializeField] private Inventory m_inventory;
        [Space]
        [SerializeField] private ItemFieldUI m_itemFieldPrefab;
        [SerializeField] private Transform m_itemFieldsContainer;

        public Inventory inventory
        {
            get => m_inventory;
            set
            {
                // ������� �� ������� ������� ���������
                if (m_inventory)
                    m_inventory.OnItemsListUpdated.RemoveListener(UpdateFields);

                m_inventory = value;

                // �� ��������� UI, ���� ��� �� �������. ���������� ���������� ��� ��������� �������
                if (gameObject.activeInHierarchy)
                {
                    // �������� �� ������� ������ ���������
                    if (m_inventory)
                        m_inventory.OnItemsListUpdated.AddListener(UpdateFields);

                    UpdateFields();
                }
            }
        }

        private IObjectPool<ItemFieldUI> _fieldsPool;

        private void Start()
        {
            // �������� ���� ����� ��������� � ���������� � �������� ��������
            _fieldsPool = new ObjectPool<ItemFieldUI>(
                createFunc: () =>
                {
                    ItemFieldUI field = Instantiate(m_itemFieldPrefab, m_itemFieldsContainer);
                    return field;
                },
                actionOnGet: (itemField) =>
                {
                    itemField.gameObject.SetActive(true);
                },
                actionOnRelease: (itemField) =>
                {
                    itemField.gameObject.SetActive(false);
                }
            );
            UpdateFields();
        }

        private void OnEnable()
        {
            if (m_inventory)
                m_inventory.OnItemsListUpdated.AddListener(UpdateFields);

            UpdateFields();
        }

        private void OnDisable()
        {
            if (m_inventory)
                m_inventory.OnItemsListUpdated.RemoveListener(UpdateFields);
        }

        private void UpdateFields()
        {
            if (_fieldsPool == null)
                return;

            // �������� ��� �������� ������ ���������
            foreach (var item in m_itemFieldsContainer.GetComponentsInChildren<ItemFieldUI>())
                if (item.gameObject.activeSelf)
                    _fieldsPool.Release(item);

            if (m_inventory == null)
                return;

            foreach (var item in inventory.Items)
            {
                ItemFieldUI field = _fieldsPool.Get();
                field.SetItem(item);
            }
        }

    }

}