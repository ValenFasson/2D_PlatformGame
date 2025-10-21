using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFacade : MonoBehaviour
{
    private S_SoundSystem soundSystem;
    private S_AnimationSystem animationSystem;
    private S_VisualEffectSystem visualEffectSystem;

    public EnemyFacade(S_SoundSystem soundSys, S_AnimationSystem animationSys, S_VisualEffectSystem visualEffectSys)
    {
        this.soundSystem = soundSys;
        this.animationSystem = animationSys;
        this.visualEffectSystem = visualEffectSys;
    }

    public void ExecuteEnemy(string enemyName)
    {
        soundSystem.PlaySound(enemyName + "_enemy");
        animationSystem.PlayAnimation(enemyName + "_enemy");
        visualEffectSystem.PlayEffect(enemyName + "_enemy");
    }
}
