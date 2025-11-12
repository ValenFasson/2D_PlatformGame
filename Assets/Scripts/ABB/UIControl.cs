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

    [SerializeField] public NodoABB nodo;

    public void Update()
    {
        valorEnPantalla.text = rndValue.ToString();
        if (!valueAssigned)
        {
            rndValue = Number();
            //Arbol.AgregarElem(ref Arbol.raiz, rndValue);
            nodo = Arbol.raiz;
            valueAssigned = true;
            Debug.Log("Número asignado: " + rndValue);
        }


        if (Input.GetMouseButtonDown(0)) //LMB
        {
            Debug.Log("LMB");
            nodo = Arbol.HijoIzq(nodo);
            Info();
        }
        if (Input.GetMouseButtonDown(1)) //RMB
        {
            Debug.Log("RMB");
            nodo = Arbol.HijoDer(nodo);
            Info();
        }
        if (Input.GetKey(KeyCode.R) && nodo != Arbol.raiz) //LMB
        {
            if(nodo == null) 
            {
                nodo = Arbol.raiz;
            }
            Debug.Log("Guardado");
            nodo.info = rndValue;
            valueAssigned = false;

        }
    }

    public void Info() 
    {
        Debug.Log($"Estoy en el nodo{nodo.info}");
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
        // ?? Rango total posible: 1–9
        int totalNumerosPosibles = 9;

        // ?? Si ya se usaron todos los números, cortamos
        if (AllValues.Count >= totalNumerosPosibles)
        {
            Debug.LogWarning("Ya se usaron todos los números posibles (1–9)");
            return -1; // o podés devolver 0 o cualquier marcador
        }

        int newValue;
        do
        {
            newValue = rnd.Next(1, 10);
        }
        while (AllValues.Contains(newValue));

        AllValues.Add(newValue);
        return newValue;
    }

}
