using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColaTDA
{
    void InicializarCola();
    
    void Acolar(string name);
    
    void Desacolar();
    
    bool ColaVacia();
    
    string Primero();
}
