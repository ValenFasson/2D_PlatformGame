using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodoABB : MonoBehaviour
{
    [SerializeField] public int info;
    [SerializeField] public NodoABB hijoIzq;
    [SerializeField] public NodoABB hijoDer;
    [SerializeField] public TextMeshProUGUI TMP;
    public void Update()
    {
        TMP.text = info.ToString();
    }
}
