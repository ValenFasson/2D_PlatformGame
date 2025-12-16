using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomResultsPopUp : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] GameObject popUpBg;

    [Header("Current Score")]
    [SerializeField] TMP_Text currentScoreLabel;
    [SerializeField] TMP_Text currentScoreValue;

    [Header("Room Operations")]
    [SerializeField] TMP_Text operationsLabel;
    [SerializeField] TMP_Text operationsText;

    [Header("Room Result")]
    [SerializeField] TMP_Text roomResultLabel;
    [SerializeField] TMP_Text roomResultValue;

    [Header("Final Score")]
    [SerializeField] TMP_Text finalScoreLabel;
    [SerializeField] TMP_Text finalScoreValue;

    [Header("Button")]
    [SerializeField] Button continueButton;

    SceneStackManager sceneStack;

    void Awake()
    {
        sceneStack = FindObjectOfType<SceneStackManager>();
        HideAll();
        gameObject.SetActive(false);
    }

    void HideAll()
    {
        popUpBg.SetActive(false);

        currentScoreLabel.gameObject.SetActive(false);
        currentScoreValue.gameObject.SetActive(false);

        operationsLabel.gameObject.SetActive(false);
        operationsText.gameObject.SetActive(false);

        roomResultLabel.gameObject.SetActive(false);
        roomResultValue.gameObject.SetActive(false);

        finalScoreLabel.gameObject.SetActive(false);
        finalScoreValue.gameObject.SetActive(false);

        continueButton.gameObject.SetActive(false);
    }

    public void Show()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        HideAll();

        Time.timeScale = 0f;
        StartCoroutine(ShowSequence());
    }

    IEnumerator ShowSequence()
    {
        var s = Singleton.instance;

        int currentScore = s.previousScore;
        int roomResult = Mathf.RoundToInt(s.pilaScore);
        int finalScore = currentScore + roomResult;

        // 1️⃣ Background
        popUpBg.SetActive(true);
        yield return new WaitForSecondsRealtime(0.25f);

        // 2️⃣ Puntaje actual (label)
        currentScoreLabel.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.15f);

        // 3️⃣ Puntaje actual (valor animado)
        currentScoreValue.gameObject.SetActive(true);
        yield return CountNumber(currentScoreValue, 0, currentScore, 0.6f);

        // 4️⃣ Cálculos del nivel (label)
        operationsLabel.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.15f);

        // 5️⃣ Operaciones (sin animación)
        operationsText.gameObject.SetActive(true);
        operationsText.text = s.roomOperations;
        yield return new WaitForSecondsRealtime(0.25f);

        // 6️⃣ Resultado del nivel (label)
        roomResultLabel.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.15f);

        // 7️⃣ Resultado del nivel (valor animado)
        roomResultValue.gameObject.SetActive(true);
        yield return CountNumber(roomResultValue, 0, roomResult, 0.6f);

        // 8️⃣ Puntaje final (label)
        finalScoreLabel.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.15f);

        // 9️⃣ Puntaje final (valor animado)
        finalScoreValue.gameObject.SetActive(true);
        yield return CountNumber(finalScoreValue, currentScore, finalScore, 0.8f);

        // 🔟 Botón
        continueButton.gameObject.SetActive(true);
    }

    IEnumerator CountNumber(TMP_Text text, int from, int to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            int value = Mathf.RoundToInt(Mathf.Lerp(from, to, elapsed / duration));
            text.text = value.ToString();
            yield return null;
        }

        text.text = to.ToString();
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        StopAllCoroutines();
        gameObject.SetActive(false);
        sceneStack.GetScene();
    }
}
