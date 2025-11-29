using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cola : IColaTDA
{
    string[] scenesNames;
    int indice;

    public void InicializarCola()
    {
        scenesNames = new string[20];
        indice = 0;
    }

    public void Acolar(string name)
    {
        scenesNames[indice] = name; // agregar al final
        indice++;
    }

    public void Desacolar()
    {
        for (int i = 0; i < indice - 1; i++)
        {
            scenesNames[i] = scenesNames[i + 1]; // correr todo a la izquierda
        }
        indice--;
    }

    public bool ColaVacia()
    {
        return indice == 0;
    }

    public string Primero()
    {
        return scenesNames[0]; // el primero en entrar
    }
}
