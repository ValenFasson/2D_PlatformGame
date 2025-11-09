using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ScoreSystem : MonoBehaviour
{
    [SerializeField] public Singleton singleton;
    public void AddScore(int amount)
    {
        singleton.CurrentScore += amount;
    }
}
