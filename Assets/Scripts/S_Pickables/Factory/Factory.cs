using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public Dictionary<string, ItemHerency> items;
    public Transform spawnPoint;
    public ItemHerency Create(string id) 
    {
        if(!items.TryGetValue(id, out ItemHerency item)) 
        {
            return null;
        }
        return Instantiate(item, spawnPoint.position, spawnPoint.rotation);
    }

    public void Awake()
    {
        items = new Dictionary<string, ItemHerency>();
    }
}
