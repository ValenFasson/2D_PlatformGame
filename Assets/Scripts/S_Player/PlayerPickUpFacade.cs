using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Facade component for player to trigger pickup behaviour
public class PlayerPickUpFacade : MonoBehaviour
{
    private PickUpFacade pickUpFacade;

    private void Start()
    {
        
        S_SoundSystem soundSystem = new S_SoundSystem();
        S_AnimationSystem animationSystem = new S_AnimationSystem();
        S_VisualEffectSystem visualEffectSystem = new S_VisualEffectSystem();

        pickUpFacade = new PickUpFacade(soundSystem, animationSystem, visualEffectSystem);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickupItem"))
        {
            string itemName = other.gameObject.name;
            pickUpFacade.ExecutePickup(itemName);
            
        }
    }
}
