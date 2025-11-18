using UnityEngine;

public class BubbleShield : ItemHerency, IItemStore
{
    public int priceId { get; set; }
    public string ItemId { get; set; }
    public SpriteRenderer Renderer { get; set; }

    private void Start()
    {
        priceId = ItemPrice;
        ItemId = ItemName;
        //GameEvents.ShieldStateChanged(true);   // ACTIVADO
    }

    private void OnDisable()
    {
        //GameEvents.ShieldStateChanged(false);  // DESACTIVADO
    }

    public void PickupItem()
    {
        S_InventoryManager.instance.AddItem(this);
        Destroy(gameObject); ;
    }
    public void UseItem()
    {
        Debug.Log("Using Bubble Shield Item");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag ("Player"))
        {
            PickupItem();

        }
    }
}
