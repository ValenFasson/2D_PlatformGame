using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IABBTDA
{
    int Raiz();
    public NodoABB HijoIzq(NodoABB nodo);
    public NodoABB HijoDer(NodoABB nodo);
    bool ArbolVacio();
    void InicializarArbol();
    void AgregarElem(ref NodoABB n, int x);
    void EliminarElem(ref NodoABB n, int x);
}
