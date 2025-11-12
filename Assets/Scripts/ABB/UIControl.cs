using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [SerializeField] public ABB Arbol;
    System.Random rnd = new System.Random();
    private int rndValue;
    private List<int> AllValues;
    private bool valueAssigned = false;

    [SerializeField] private TextMeshProUGUI valorEnPantalla;
    public void Game()
    {
        if (!valueAssigned) // ?? Solo entra una vez
        {
            rndValue = Number();
            Arbol.AgregarElem(ref Arbol.raiz, rndValue);
            valueAssigned = true; // ?? bloquea futuras asignaciones
            Debug.Log("Número asignado: " + rndValue);
        }
        else
        {
            Debug.Log("Ya se asignó un valor previamente.");
        }
    }

    public void Update()
    {
        valorEnPantalla.text = rndValue.ToString();
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
        int newValue;
        do
        {
            newValue = rnd.Next(1, 10);
        }
        while (AllValues.Contains(newValue)); // si ya está, vuelve a generar

        AllValues.Add(newValue); // lo guardamos para evitar repetición futura
        return newValue;
    }

}
