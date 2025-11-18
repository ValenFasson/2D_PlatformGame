using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public IItemStore[] itemsArry;
    public Dictionary<string, IItemStore> items;
    public IItemStore Create(string id) 
    {
        if(!items.TryGetValue(id, out IItemStore item)) 
        {
            return null;
        }
        return null;
    }

    public void Awake()
    {
        items = new Dictionary<string, IItemStore>();
    }
}
