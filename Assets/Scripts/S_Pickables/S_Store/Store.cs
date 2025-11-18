using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public ItemHerency[] itemsInStore;
    public int index;
    public TextMeshProUGUI BuyButton;
    public Factory factory;

   
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
        BuyButton.text = itemsInStore[index].ItemName + " = " + itemsInStore[index].ItemPrice.ToString();
    }

    public void Buy() 
    {
        Debug.Log("esto deberia estar saliendo aunque con un error, faltan los items");
//        Singleton.instance.CurrentScore -= itemsInStore[index].ItemPrice;
        factory.Create(itemsInStore[index].ItemName);
    }
}
