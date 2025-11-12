using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABB : MonoBehaviour , IABBTDA
{
    [SerializeField] public NodoABB raiz;

    /*
    static int altura(NodoABB ab)
    {
        if (ab == null)
        {
            return -1;
        }
        else
        {
            return (1 + Math.Max(altura(ab.hijoIzq), altura(ab.hijoDer)));
        }
    }

    static void preOrder_FE(NodoABB a)
    {
        if (a != null)
        {
            // accion mientras recorro //
            Console.WriteLine("Nodo Padre: " + a.info.ToString());
            Console.WriteLine("Altura Izquierda: " + altura(a.hijoDer));
            Console.WriteLine("Altura Derecha: " + altura(a.hijoIzq));
            Console.WriteLine();
            //                         //

            preOrder_FE(a.hijoIzq);
            preOrder_FE(a.hijoDer);
        }
    }

    static void preOrder(NodoABB a)
    {
        if (a != null)
        {
            Console.WriteLine(a.info.ToString());
            preOrder(a.hijoIzq);
            preOrder(a.hijoDer);
        }
    }

    static void inOrder(NodoABB a)
    {
        if (a != null)
        {
            inOrder(a.hijoIzq);
            Console.WriteLine(a.info.ToString());
            inOrder(a.hijoDer);
        }
    }

    static void postOrder(NodoABB a)
    {
        if (a != null)
        {
            postOrder(a.hijoIzq);
            postOrder(a.hijoDer);
            Console.WriteLine(a.info.ToString());
        }
    }

    static void level_Order(NodoABB nodo)
    {
        Queue<NodoABB> q = new Queue<NodoABB>();

        q.Enqueue(nodo);

        while (q.Count > 0)
        {
            nodo = q.Dequeue();

            Console.WriteLine(nodo.info.ToString());

            if (nodo.hijoIzq != null) { q.Enqueue(nodo.hijoIzq); }

            if (nodo.hijoDer != null) { q.Enqueue(nodo.hijoDer); }
        }
    }

    static void levelOrder(NodoABB nodo)
    {
        Queue<NodoABB> q = new Queue<NodoABB>();

        q.Enqueue(nodo);

        while (q.Count > 0)
        {
            nodo = q.Dequeue();

            Console.WriteLine("Padre: " + nodo.info.ToString());

            if (nodo.hijoIzq != null)
            {
                q.Enqueue(nodo.hijoIzq);
                Console.WriteLine("Hijo Izq: " + nodo.hijoIzq.info.ToString());
            }
            else
            {
                Console.WriteLine("Hijo Izq: null");
            }

            if (nodo.hijoDer != null)
            {
                q.Enqueue(nodo.hijoDer);
                Console.WriteLine("Hijo Der: " + nodo.hijoDer.info.ToString());
            }
            else
            {
                Console.WriteLine("Hijo Der: null");
            }
        }
    }
    */
    public int Raiz()
    {
        return raiz.info;
    }

    public bool ArbolVacio()
    {
        return (raiz == null);
    }

    public void InicializarArbol()
    {
        raiz = null;
    }

    public NodoABB HijoDer()
    {
        return raiz.hijoDer;
    }

    public NodoABB HijoIzq()
    {
        return raiz.hijoIzq;
    }

    public void AgregarElem(ref NodoABB raiz, int x)
    {
        if (raiz == null)
        {
            raiz = new NodoABB();
            raiz.info = x;
        }
        else if (raiz.info > x)
        {
            AgregarElem(ref raiz.hijoIzq, x);
        }
        else if (raiz.info < x)
        {
            AgregarElem(ref raiz.hijoDer, x);
        }
    }

    public void EliminarElem(ref NodoABB raiz, int x)
    {
        if (raiz != null)
        {
            if (raiz.info == x && (raiz.hijoIzq == null) && (raiz.hijoDer == null))
            {
                raiz = null;
            }
            else if (raiz.info == x && raiz.hijoIzq != null)
            {
                raiz.info = this.mayor(raiz.hijoIzq);
                EliminarElem(ref raiz.hijoIzq, raiz.info);
            }
            else if (raiz.info == x && raiz.hijoIzq == null)
            {
                raiz.info = this.menor(raiz.hijoDer);
                EliminarElem(ref raiz.hijoDer, raiz.info);
            }
            else if (raiz.info < x)
            {
                EliminarElem(ref raiz.hijoDer, x);
            }
            else
            {
                EliminarElem(ref raiz.hijoIzq, x);
            }
        }
    }

    public int mayor(NodoABB a)
    {
        if (a.hijoDer == null)
        {
            return a.info;
        }
        else
        {
            return mayor(a.hijoDer);
        }
    }

    public int menor(NodoABB a)
    {
        if (a.hijoIzq == null)
        {
            return a.info;
        }
        else
        {
            return menor(a.hijoIzq);
        }
    }

}