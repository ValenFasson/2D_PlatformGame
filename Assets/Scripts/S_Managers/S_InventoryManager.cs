using UnityEngine;

public class S_InventoryManager : MonoBehaviour
{
    public static S_InventoryManager instance;

    public IItemStore slot1;
    public IItemStore slot2;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(IItemStore item)
    {
        if (slot1 == null)
        {
            slot1 = item;
            BroadcastUI(item, 1);
            return;
        }

        if (slot2 == null)
        {
            slot2 = item;
            BroadcastUI(item, 2);
            return;
        }

        // Si ambos llenos → reemplaza slot2 (opcional)
        slot2 = item;
        BroadcastUI(item, 2);
    }

    void BroadcastUI(IItemStore item, int slotIndex)
    {
        switch (item.ItemId)
        {
            case "shield":
                GameEvents.ShieldStateChanged(true);
                break;

            case "velocity":
                GameEvents.SpeedStateChanged(true);
                break;
        }
    }
}
