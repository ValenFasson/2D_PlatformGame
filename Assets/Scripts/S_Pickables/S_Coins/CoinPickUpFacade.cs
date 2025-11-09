using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUpFacade : MonoBehaviour // <----- Este es el cliente
{
    private PickUpFacade pickUpFacade; // <----- Esta la fachada 

    public S_SoundSystem inspectorSoundSystem; // <----- Estos los sub sistemas
    public S_AnimationSystem inspectorAnimationSystem;
    public S_VisualEffectSystem inspectorVisualEffectSystem;
    public S_ScoreSystem inspectorScoreSystem;

    //Los 3 componentes del patron Facade (nada de components ni managers)
    //Cada cliente debe tener los sistemas que aplique el facade

    [SerializeField] private int scoreValueForThisCoin;

    private void Start()
    {
        //Busca los subsistemas en escena o si ya los tienen asignados (NO LOS CREA), los subsistemas ya deben existir en escena ahi viven
        var soundSystem = inspectorSoundSystem != null ? inspectorSoundSystem : FindObjectOfType<S_SoundSystem>();
        var animationSystem = inspectorAnimationSystem != null ? inspectorAnimationSystem : FindObjectOfType<S_AnimationSystem>();
        var visualEffectSystem = inspectorVisualEffectSystem != null ? inspectorVisualEffectSystem : FindObjectOfType<S_VisualEffectSystem>();
        var scoreSystem = inspectorScoreSystem != null ? inspectorScoreSystem : FindObjectOfType<S_ScoreSystem>();
        pickUpFacade = new PickUpFacade(soundSystem, animationSystem, visualEffectSystem, scoreSystem);
        pickUpFacade.scoreAux = scoreValueForThisCoin;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string itemName = other.gameObject.name;
            pickUpFacade.ExecutePickup(itemName);
            transform.parent.gameObject.SetActive(false); // con esto desactivamos el objeto padre de quien posea este script
        }
    }
}
