using System.Collections;
using System.Collections.Generic;
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
    }
    private void Start()
    {
        priceId = ItemPrice;
        ItemId = ItemName;
    }

    public void PickupItem()
    {
        Debug.Log("Picked up Velocity Power-Up Item");
    }

    public void UseItem()
    {
        Debug.Log("Using Velocity Power-Up Item");
    }
}
