using InteractiveSystem;
using InventorySystem;
using UnityEngine;
using UnityEngine.Events;

public class FarmingBuilding : InteractableObject
{

    [field: SerializeField] public string FarmItemID { get; private set; }
    [field: SerializeField] public float ItemSpawnSpeed { get; private set; }

    public int CurrentItemsCount { get; private set; }
    public float NextItemTimer { get; private set; }

    public UnityEvent<int> OnItemsCountChanged { get; private set; } = new UnityEvent<int>();

    protected virtual void Start()
    {
        NextItemTimer = ItemSpawnSpeed;
    }

    protected virtual void Update()
    {
        NextItemTimer -= Time.deltaTime;
        if (NextItemTimer <= 0)
        {
            NextItemTimer = ItemSpawnSpeed;
            AddItem(1);
        }
    }

    public override void Interact(GameObject interactor)
    {
        base.Interact(interactor);

        if (interactor.TryGetComponent(out Inventory inventory))
            CollectItems(inventory);
    }

    public void CollectItems(Inventory inventory)
    {
        if (CurrentItemsCount <= 0)
            return;
        inventory.AddItem(FarmItemID, CurrentItemsCount);
        CurrentItemsCount = 0;
        OnItemsCountChanged?.Invoke(CurrentItemsCount);
    }

    private void AddItem(int count)
    {
        CurrentItemsCount += count;
        OnItemsCountChanged?.Invoke(CurrentItemsCount);
    }

}
