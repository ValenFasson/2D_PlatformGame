using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO_Reaper", menuName = "ScriptableObjects/enemies/Reaper", order = 2)]
public class SO_Reaper : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float ThresholdDistance;
}
