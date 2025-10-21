using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO_Reaper", menuName = "ScriptableObjects/enemies/Reaper", order = 2)]
public class SO_Reaper : ScriptableObject
{
    [SerializeField] public float speed;
    [SerializeField] public float ThresholdDistance;
}
