using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_EyeTurret", menuName = "ScriptableObjects/enemies/EyeTurret", order = 1)]
public class SO_EyeTurret : ScriptableObject
{
    [SerializeField] int speed;
}
