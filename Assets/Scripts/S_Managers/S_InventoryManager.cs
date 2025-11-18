using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_InventoryManager : MonoBehaviour
{

    static public S_InventoryManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(IItemStore Item)
    {
        switch (Item.ItemId)
        {
            case "shield":
                GameEvents.ShieldStateChanged(true);
                break;


            case "velocity":
                GameEvents.SpeedStateChanged(true);  // ACTIVADO
                break;
        }
    }



}


