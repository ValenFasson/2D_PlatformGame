using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Facade component for player to trigger pickup behaviour
public class PlayerPickUpFacade : MonoBehaviour
{
    private PickUpFacade pickUpFacade;

    // Optional: allow designers to assign systems in the inspector
    public S_SoundSystem inspectorSoundSystem;
    public S_AnimationSystem inspectorAnimationSystem;
    public S_VisualEffectSystem inspectorVisualEffectSystem;

    private void Start()
    {
        var soundSystem = inspectorSoundSystem != null ? inspectorSoundSystem : FindObjectOfType<S_SoundSystem>();
        var animationSystem = inspectorAnimationSystem != null ? inspectorAnimationSystem : FindObjectOfType<S_AnimationSystem>();
        var visualEffectSystem = inspectorVisualEffectSystem != null ? inspectorVisualEffectSystem : FindObjectOfType<S_VisualEffectSystem>();
        pickUpFacade = new PickUpFacade(soundSystem, animationSystem, visualEffectSystem);
        Debug.Log($"PlayerPickUpFacade.Start: Found systems - Sound={(soundSystem!=null)}, Animation={(animationSystem!=null)}, VFX={(visualEffectSystem!=null)}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"PlayerPickUpFacade.OnTriggerEnter2D: other={other?.gameObject?.name} tag={other?.tag}");
        if (other.CompareTag("PickupItem"))
        {
            string itemName = other.gameObject.name;
            pickUpFacade.ExecutePickup(itemName);
            
        }
    }
}
