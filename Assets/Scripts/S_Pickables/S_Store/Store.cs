using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public IItemStore[] itemsInStore;
    public int index;
    public TextMeshProUGUI BuyButton;
    public Factory factory;

    public void Start()
    {
        
         itemsInStore = new IItemStore[]
        {
            new BubbleShield(),   // index 0
            new VelocityPowerUp()     // index 1
            
        };
         
    }
    public void MoveLeft() 
    {
        Debug.Log("El index Decrementa");
        if( index == 0) 
        {
            index = itemsInStore.Length;
        }
        else 
        {
            index--;
        }
        ChangeButtonInfo();
    }
    public void MoveRight() 
    {
        Debug.Log("El index incrementa");
        if(index % itemsInStore.Length == 0 )
        {
            index = 0;
        }
        else 
        {
            index++;
        }
        ChangeButtonInfo();
    }

    public void ChangeButtonInfo() 
    {
        BuyButton.text = itemsInStore[index].ItemId + " = " + itemsInStore[index].priceId.ToString();
    }

    public void Buy() 
    {
        Debug.Log("esto deberia estar saliendo aunque con un error, faltan los items");
        Singleton.instance.CurrentScore -= itemsInStore[index].priceId;
        factory.Create(itemsInStore[index].ItemId);
    }
}
