using FloatingUtils;
using UnityEngine;

public class FarmingItemsHud : MonoBehaviour
{

    [SerializeField] private FloatingFarmingItemsHud m_floatingHud;
    [Space]
    [SerializeField] private FarmingBuilding m_farmingBuilding;

    private void Start()
    {
        // Создание и привязка интерфейса к зданию
        FloatingFarmingItemsHud hud = (FloatingFarmingItemsHud)FloatingSpawner.Instance.SpawnObject(m_floatingHud);
        hud.farmingBuilding = m_farmingBuilding;
    }

}
