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
        }
        if (Input.GetMouseButtonDown(1)) //RMB
        {
            Debug.Log("RMB");
            nodo = Arbol.HijoDer(nodo);
        }
        if (Input.GetKey(KeyCode.R)) //LMB
        {
            Debug.Log("Guardado");
            nodo.info = rndValue;
            valueAssigned = false;

        }
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
