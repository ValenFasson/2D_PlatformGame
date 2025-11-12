using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [SerializeField] public ABB Arbol;
    System.Random rnd = new System.Random();
    private int rndValue;
    private List<int> AllValues;
    public void Game() 
    {

    }
    public void Awake()
    {
        AllValues = new List<int>();    
        rndValue = rnd.Next(1, 10);
        Arbol.raiz.info = rndValue;
        AllValues.Add(rndValue);
    }

    public int Number() 
    {
        rndValue = rnd.Next(1, 10);
        foreach (int value in AllValues) 
        {
            return value;
        }
        return 0;
    }
}
