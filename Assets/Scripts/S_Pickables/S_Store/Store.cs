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
        //aca tenemos que inicializar los items en el array de esta manera:
        /*
         IItemStore[] itemsInStore = new IItemStore[]
        {
            new ScriptNameItem1(),   // index 0
            new ScriptNameItem2(),     // index 1
            new ScriptNameItem3() // index 2
        };
         */
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
