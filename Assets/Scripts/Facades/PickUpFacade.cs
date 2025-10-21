using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFacade : MonoBehaviour
{
    private SoundSystem soundSystem;
    private AnimationSystem animationSystem;
    private VisualEffectSystem visualEffectSystem;


    public PickUpFacade(SoundSystem soundSys, AnimationSystem animationSys, VisualEffectSystem visualEffectSys)
    {
        this.soundSystem = soundSys;
        this.animationSystem = animationSys;
        this.visualEffectSystem = visualEffectSys;
    }

    public void ExecutePickup(string itemName)
    {
        soundSystem.PlaySound(itemName + "_pickup");
        animationSystem.PlayAnimation(itemName + "_pickup");
        visualEffectSystem.PlayEffect(itemName + "_pickup");
    }

    
}
