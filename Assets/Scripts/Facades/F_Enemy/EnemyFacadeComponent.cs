using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFacadeComponent : MonoBehaviour
{
    private EnemyFacade facade;

    // Optional: allow designers to assign manager systems in the inspector
    public S_SoundSystem inspectorSoundSystem;
    public S_AnimationSystem inspectorAnimationSystem;
    public S_VisualEffectSystem inspectorVisualEffectSystem;

    void Awake()
    {
        // If a central manager exists, we'll use it. Otherwise create a local facade.
        if (EnemyFacadeManager.Instance != null)
        {
            Debug.Log("EnemyFacadeComponent: Using central EnemyFacadeManager instance.");
            // local facade left null; ExecuteEnemy will route to manager
            return;
        }

        var soundSystem = inspectorSoundSystem != null ? inspectorSoundSystem : FindObjectOfType<S_SoundSystem>();
        var animationSystem = inspectorAnimationSystem != null ? inspectorAnimationSystem : FindObjectOfType<S_AnimationSystem>();
        var visualEffectSystem = inspectorVisualEffectSystem != null ? inspectorVisualEffectSystem : FindObjectOfType<S_VisualEffectSystem>();

       

        facade = new EnemyFacade(soundSystem, animationSystem, visualEffectSystem);
    }

    
    public void ExecuteEnemy(string enemyName)
    {
        Debug.Log($"EnemyFacadeComponent: ExecuteEnemy called for '{enemyName}'.");
        if (EnemyFacadeManager.Instance != null)
        {
            EnemyFacadeManager.Instance.ExecuteEnemy(enemyName);
            return;
        }

        facade?.ExecuteEnemy(enemyName);
    }
}
