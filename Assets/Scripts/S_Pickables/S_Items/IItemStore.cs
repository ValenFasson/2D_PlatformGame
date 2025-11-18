using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemStore
{
    string ItemId { get; }
    int priceId { get; }

    SpriteRenderer Renderer { get; }

    public void UseItem();

    public void PickupItem();
}
