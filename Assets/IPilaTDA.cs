using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPilaTDA
{
    void InicializarPila(); 
   
    void Apilar(Operation op);
   
    void Desapilar();
    
    bool PilaVacia();
    Operation Primero();
}
