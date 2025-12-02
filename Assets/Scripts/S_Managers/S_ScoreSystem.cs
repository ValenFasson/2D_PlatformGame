using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ScoreSystem : MonoBehaviour
{
    [SerializeField] public Singleton singleton;
    Operation Operacion;
    public void AddScore(int amount, string op)
    {
       Operacion = new Operation();
       Operacion.amount = amount;
       Operacion.op = op;
       singleton.pila.Apilar(Operacion);
    }
}

public class Operation
{
    public int amount;
    public string op;
}
