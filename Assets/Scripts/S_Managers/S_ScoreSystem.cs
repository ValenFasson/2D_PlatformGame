using UnityEngine;

public class S_ScoreSystem : MonoBehaviour
{
    [SerializeField] public Singleton singleton;
    Operation Operacion;

    public void AddScore(float amount, Enum_Coin_Operation op)
    {
        Operacion = new Operation();
        Operacion.amount = Mathf.RoundToInt(amount);
        Operacion.op = op;
        singleton.pila.Apilar(Operacion);
    }
}

public class Operation
{
    public int amount;
    public Enum_Coin_Operation op;
}
