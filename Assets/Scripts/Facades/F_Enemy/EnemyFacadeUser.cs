using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyFacadeComponent))]
public class EnemyFacadeUser : MonoBehaviour, IEnemy
{
    // Optional: explicit name used for facade calls. If empty, will use GameObject.name
    public string enemyFacadeName;

    private EnemyFacadeComponent localFacadeComponent;

    void Awake()
    {
        localFacadeComponent = GetComponent<EnemyFacadeComponent>();
    }

    public void TriggerFacade(string actionName)
    {
        string nameToUse = string.IsNullOrEmpty(enemyFacadeName) ? gameObject.name : enemyFacadeName;
        string facadeKey = nameToUse + "_" + actionName; // e.g., Reaper_attack -> Reaper_attack

        Debug.Log($"EnemyFacadeUser: Triggering facade action '{facadeKey}'");

        if (EnemyFacadeManager.Instance != null)
        {
            EnemyFacadeManager.Instance.ExecuteEnemy(facadeKey);
            return;
        }

        if (localFacadeComponent != null)
        {
            localFacadeComponent.ExecuteEnemy(facadeKey);
            return;
        }

        Debug.LogWarning("EnemyFacadeUser: No facade manager or component available to route the call.");
    }
}
