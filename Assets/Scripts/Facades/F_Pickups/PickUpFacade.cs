using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFacade
{
    private S_SoundSystem soundSystem;
    private S_AnimationSystem animationSystem;
    private S_VisualEffectSystem visualEffectSystem;
    private S_ScoreSystem scoreSystem;

    [NonSerialized] public float scoreAux;
    [NonSerialized] public Enum_Coin_Operation opAux; //tipo de operacion

    public PickUpFacade(S_SoundSystem soundSys, S_AnimationSystem animationSys, S_VisualEffectSystem visualEffectSys, S_ScoreSystem scoreSys)
    {
        //Cuando el cliente crea su fachada asigna a esta fachada los subsistemas que obtuvo
        this.soundSystem = soundSys;
        this.animationSystem = animationSys;
        this.visualEffectSystem = visualEffectSys;
        this.scoreSystem = scoreSys;
    }

    public void ExecutePickup(string itemName)
    { 
        Debug.Log($"PickUpFacade: ExecutePickup called for '{itemName}'. SoundSystem={(soundSystem!=null)}, AnimationSystem={(animationSystem!=null)}, VFXSystem={(visualEffectSystem!=null)}");
        soundSystem?.PlaySound(itemName + "_pickup");
        animationSystem?.PlayAnimation(itemName + "_pickup");
        visualEffectSystem?.PlayEffect(itemName + "_pickup");
        scoreSystem?.AddScore(scoreAux, opAux);
    }
}
