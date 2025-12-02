using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pila : IPilaTDA
{
    Operation[] Op; // arreglo en donde se guarda la informacion
    int indice; // variable entera en donde se guarda la cantidad de elementos que se tienen guardados
    public void InicializarPila()
    {
        Op = new Operation[100];
        indice = 0;
    }

    public void Apilar(Operation op)
    {
        for (int i = indice - 1; i >= 0; i--)
        {
            Op[i + 1] = Op[i];
        }
        Op[0] = op;

        indice++;
    }
    public void Desapilar()
    {
        indice--;
    }
    public bool PilaVacia()
    {
        return (indice == 0);
    }
    public Operation Primero()
    {
        return Op[indice - 1];
    }
}
