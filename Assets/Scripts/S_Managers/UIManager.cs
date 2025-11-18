using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;
    public GameObject shieldIcon;
    public GameObject speedIcon;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        GameEvents.OnPlayerDamaged += UpdateHealth;
        GameEvents.OnShieldStateChanged += SetShieldUI;
        GameEvents.OnSpeedStateChanged += SetSpeedUI;

        GameEvents.OnShieldUsed += HideShieldUI;
        GameEvents.OnSpeedUsed += HideSpeedUI;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerDamaged -= UpdateHealth;
        GameEvents.OnShieldStateChanged -= SetShieldUI;
        GameEvents.OnSpeedStateChanged -= SetSpeedUI;

        GameEvents.OnShieldUsed -= HideShieldUI;
        GameEvents.OnSpeedUsed -= HideSpeedUI;
    }

    void Start()
    {
        var player = FindObjectOfType<PlayerHealth>();
        if (player != null)
            healthText.text = player.currentHealth.ToString();

        shieldIcon.SetActive(false);
        speedIcon.SetActive(false);
    }

    void UpdateHealth(int dmg, int currentHealth)
    {
        healthText.text = currentHealth.ToString();
    }

    void SetShieldUI(bool active)
    {
        shieldIcon.SetActive(active);
    }

    void SetSpeedUI(bool active)
    {
        speedIcon.SetActive(active);
    }


    void HideShieldUI()
    {
        shieldIcon.SetActive(false);
    }

    void HideSpeedUI()
    {
        speedIcon.SetActive(false);
    }

}
