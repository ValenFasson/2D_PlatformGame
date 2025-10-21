using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Component wrapper for EnemyFacade. Attach to enemy prefabs or manager objects.
public class EnemyFacadeComponent : MonoBehaviour
{
    private EnemyFacade facade;

    // Optional: allow designers to assign manager systems in the inspector
    public S_SoundSystem inspectorSoundSystem;
    public S_AnimationSystem inspectorAnimationSystem;
    public S_VisualEffectSystem inspectorVisualEffectSystem;

    void Awake()
    {
        var soundSystem = inspectorSoundSystem != null ? inspectorSoundSystem : FindObjectOfType<S_SoundSystem>();
        var animationSystem = inspectorAnimationSystem != null ? inspectorAnimationSystem : FindObjectOfType<S_AnimationSystem>();
        var visualEffectSystem = inspectorVisualEffectSystem != null ? inspectorVisualEffectSystem : FindObjectOfType<S_VisualEffectSystem>();

        if (soundSystem == null) Debug.LogWarning("EnemyFacadeComponent: S_SoundSystem not found in scene.");
        if (animationSystem == null) Debug.LogWarning("EnemyFacadeComponent: S_AnimationSystem not found in scene.");
        if (visualEffectSystem == null) Debug.LogWarning("EnemyFacadeComponent: S_VisualEffectSystem not found in scene.");

        facade = new EnemyFacade(soundSystem, animationSystem, visualEffectSystem);
    }

    // Public method enemies can call
    public void ExecuteEnemy(string enemyName)
    {
        Debug.Log($"EnemyFacadeComponent: ExecuteEnemy called for '{enemyName}'.");
        facade.ExecuteEnemy(enemyName);
    }
}
