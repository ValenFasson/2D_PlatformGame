using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Central manager for enemy facade calls. Place one instance in the scene (e.g., on GameSystems prefab).
public class EnemyFacadeManager : MonoBehaviour
{
    public static EnemyFacadeManager Instance { get; private set; }

    private EnemyFacade facade;

    // Optional inspector-assigned manager references
    public S_SoundSystem inspectorSoundSystem;
    public S_AnimationSystem inspectorAnimationSystem;
    public S_VisualEffectSystem inspectorVisualEffectSystem;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        var soundSystem = inspectorSoundSystem != null ? inspectorSoundSystem : FindObjectOfType<S_SoundSystem>();
        var animationSystem = inspectorAnimationSystem != null ? inspectorAnimationSystem : FindObjectOfType<S_AnimationSystem>();
        var visualEffectSystem = inspectorVisualEffectSystem != null ? inspectorVisualEffectSystem : FindObjectOfType<S_VisualEffectSystem>();

        if (soundSystem == null) Debug.LogWarning("EnemyFacadeManager: S_SoundSystem not found in scene.");
        if (animationSystem == null) Debug.LogWarning("EnemyFacadeManager: S_AnimationSystem not found in scene.");
        if (visualEffectSystem == null) Debug.LogWarning("EnemyFacadeManager: S_VisualEffectSystem not found in scene.");

        facade = new EnemyFacade(soundSystem, animationSystem, visualEffectSystem);

        // Keep manager across scenes if desirable
        DontDestroyOnLoad(this.gameObject);
    }

    public void ExecuteEnemy(string enemyName)
    {
        Debug.Log($"EnemyFacadeManager: ExecuteEnemy('{enemyName}')");
        facade?.ExecuteEnemy(enemyName);
    }
}
