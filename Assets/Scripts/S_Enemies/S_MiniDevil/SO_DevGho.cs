using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_DevGho", menuName = "ScriptableObjects/enemies/DevGho", order = 1)]
public class SO_DevGho : ScriptableObject
{
    [SerializeField] public float speed;
}
