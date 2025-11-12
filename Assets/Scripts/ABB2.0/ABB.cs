using UnityEngine;

public class ABB : ABBTDA
{
    private NodoABB raiz;

    public int Raiz() => raiz.info;
    public bool ArbolVacio() => raiz == null;
    public void InicializarArbol() => raiz = null;

    public ABBTDA HijoDer() => raiz.hijoDer;
    public ABBTDA HijoIzq() => raiz.hijoIzq;

    public void AgregarElem(int x)
    {
        if (raiz == null)
        {
            raiz = new NodoABB();
            raiz.info = x;
            raiz.hijoIzq = new ABB(); raiz.hijoIzq.InicializarArbol();
            raiz.hijoDer = new ABB(); raiz.hijoDer.InicializarArbol();
        }
        else if (x < raiz.info)
            raiz.hijoIzq.AgregarElem(x);
        else if (x > raiz.info)
            raiz.hijoDer.AgregarElem(x);
    }

    public void EliminarElem(int x)
    {
        if (raiz == null) return;

        if (raiz.info == x && raiz.hijoIzq.ArbolVacio() && raiz.hijoDer.ArbolVacio())
            raiz = null;
        else if (raiz.info == x && !raiz.hijoIzq.ArbolVacio())
        {
            raiz.info = Mayor(raiz.hijoIzq);
            raiz.hijoIzq.EliminarElem(raiz.info);
        }
        else if (raiz.info == x && raiz.hijoIzq.ArbolVacio())
        {
            raiz.info = Menor(raiz.hijoDer);
            raiz.hijoDer.EliminarElem(raiz.info);
        }
        else if (x > raiz.info)
            raiz.hijoDer.EliminarElem(x);
        else
            raiz.hijoIzq.EliminarElem(x);
    }

    public int Mayor(ABBTDA a)
    {
        if (a.HijoDer().ArbolVacio()) return a.Raiz();
        return Mayor(a.HijoDer());
    }

    public int Menor(ABBTDA a)
    {
        if (a.HijoIzq().ArbolVacio()) return a.Raiz();
        return Menor(a.HijoIzq());
    }
}
