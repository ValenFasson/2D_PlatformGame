using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Coin Config")]
    [SerializeField] private bool showTextInCoin;
    [SerializeField] public TextMeshProUGUI coinText;
    [SerializeField] private bool isValueRandom;
    [SerializeField] private bool isValueNegative;
    [SerializeField] private float scoreValueForThisCoin;
    [SerializeField] private bool isOperationRandom;
    [SerializeField] private Enum_Coin_Operation operationForThisCoin; 


    public void Awake()
    {
        setCoinConfig();
        StartCoroutine(AwakeCoin());
    }
    private IEnumerator AwakeCoin()
    {
        yield return null; yield return null;yield return null;
        //Busca los subsistemas en escena o si ya los tienen asignados (NO LOS CREA), los subsistemas ya deben existir en escena ahi viven
        var soundSystem = inspectorSoundSystem != null ? inspectorSoundSystem : FindObjectOfType<S_SoundSystem>();
        var animationSystem = inspectorAnimationSystem != null ? inspectorAnimationSystem : FindObjectOfType<S_AnimationSystem>();
        var visualEffectSystem = inspectorVisualEffectSystem != null ? inspectorVisualEffectSystem : FindObjectOfType<S_VisualEffectSystem>();
        var scoreSystem = inspectorScoreSystem != null ? inspectorScoreSystem : FindObjectOfType<S_ScoreSystem>();
        pickUpFacade = new PickUpFacade(soundSystem, animationSystem, visualEffectSystem, scoreSystem);
        pickUpFacade.scoreAux = scoreValueForThisCoin;
        pickUpFacade.opAux = operationForThisCoin;  
        yield return null;
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
    private void setCoinConfig() 
    {
        if (isValueRandom) //si el numero es random set...
        {
            float value;
            if (isValueNegative) // entre 0 y 1
            {
                value = Random.Range(0f, 1f);
                value = Mathf.Round(value * 10f) / 10f;
            }
            else // entre 1 y 10
            {
                value = Random.Range(1,10);
                value = Mathf.Round(value);
            }
            scoreValueForThisCoin = value;
        }


        if(isOperationRandom) // si la operacion es random entonces guardamos op random
        {
            var values = (Enum_Coin_Operation[])System.Enum.GetValues(typeof(Enum_Coin_Operation));
            operationForThisCoin = values[Random.Range(0, values.Length)];
        }


        if (!showTextInCoin) { coinText.text = "";} // si no queremos que se muestre nada guardamos nada
        else if (isOperationRandom) { coinText.text = "?";}
        else
        {
            string operationChar = "";
            switch (operationForThisCoin)
            {
                case Enum_Coin_Operation.mult:
                    operationChar = "X";
                    break;
                case Enum_Coin_Operation.suma:
                    operationChar = "+";
                    break;
            }
            coinText.text = operationChar + scoreValueForThisCoin.ToString();
        }
    }
}
