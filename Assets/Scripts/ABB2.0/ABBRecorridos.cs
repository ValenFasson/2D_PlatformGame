using UnityEngine;

public static class ABBRecorridos
{
    public static int Altura(ABBTDA a)
    {
        if (a == null || a.ArbolVacio()) return -1;
        int hi = Altura(a.HijoIzq());
        int hd = Altura(a.HijoDer());
        return 1 + (hi > hd ? hi : hd);
    }

    public static void PreOrder(ABBTDA a)
    {
        if (a == null || a.ArbolVacio()) return;
        Debug.Log(a.Raiz());
        PreOrder(a.HijoIzq());
        PreOrder(a.HijoDer());
    }

    public static void InOrder(ABBTDA a)
    {
        if (a == null || a.ArbolVacio()) return;
        InOrder(a.HijoIzq());
        Debug.Log(a.Raiz());
        InOrder(a.HijoDer());
    }

    public static void PostOrder(ABBTDA a)
    {
        if (a == null || a.ArbolVacio()) return;
        PostOrder(a.HijoIzq());
        PostOrder(a.HijoDer());
        Debug.Log(a.Raiz());
    }

    public static void LevelOrder(ABBTDA a)
    {
        if (a == null || a.ArbolVacio()) return;

        ColaABB q = new ColaABB();
        q.InicializarCola();
        q.Acolar(a);

        while (!q.ColaVacia())
        {
            ABBTDA nodo = q.Primero();
            q.Desacolar();

            Debug.Log("Padre: " + nodo.Raiz());

            if (!nodo.HijoIzq().ArbolVacio())
            {
                q.Acolar(nodo.HijoIzq());
                Debug.Log("  Hijo Izq: " + nodo.HijoIzq().Raiz());
            }
            else Debug.Log("  Hijo Izq: null");

            if (!nodo.HijoDer().ArbolVacio())
            {
                q.Acolar(nodo.HijoDer());
                Debug.Log("  Hijo Der: " + nodo.HijoDer().Raiz());
            }
            else Debug.Log("  Hijo Der: null");
        }
    }
}
