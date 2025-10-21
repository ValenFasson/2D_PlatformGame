using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFacade
{
    private S_SoundSystem soundSystem;
    private S_AnimationSystem animationSystem;
    private S_VisualEffectSystem visualEffectSystem;

    public PickUpFacade(S_SoundSystem soundSys, S_AnimationSystem animationSys, S_VisualEffectSystem visualEffectSys)
    {
        this.soundSystem = soundSys;
        this.animationSystem = animationSys;
        this.visualEffectSystem = visualEffectSys;
    }

    public void ExecutePickup(string itemName)
    {
        
        Debug.Log($"PickUpFacade: ExecutePickup called for '{itemName}'. SoundSystem={(soundSystem!=null)}, AnimationSystem={(animationSystem!=null)}, VFXSystem={(visualEffectSystem!=null)}");

        
        soundSystem?.PlaySound(itemName + "_pickup");
        animationSystem?.PlayAnimation(itemName + "_pickup");
        visualEffectSystem?.PlayEffect(itemName + "_pickup");
    }
}
