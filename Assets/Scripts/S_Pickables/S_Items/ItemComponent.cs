using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : ItemHerency, IItemStore
{
    public int priceId { get; set; }
    public string ItemId { get; set; }

    private void Start()
    {
        priceId = ItemPrice;
        ItemId = ItemName;
    }
}
