using UnityEngine;

public class VelocityPowerUp : ItemHerency, IItemStore
{
    public int priceId { get; set; }
    public string ItemId { get; set; }
    public SpriteRenderer Renderer { get; set; }

    PlayerMovement playerMovement;

    private void OnEnable()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        //GameEvents.SpeedStateChanged(true);  // ACTIVADO
    }

    private void OnDisable()
    {
        //GameEvents.SpeedStateChanged(false); // DESACTIVADO
    }

    private void Start()
    {
        priceId = ItemPrice;
        ItemId = ItemName;
    }
    public void PickupItem()
    {
        S_InventoryManager.instance.AddItem(this);
        Destroy(gameObject);
    }

    public void UseItem()
    {
        Debug.Log("Using Velocity Power-Up Item");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickupItem();

        }
    }
}
