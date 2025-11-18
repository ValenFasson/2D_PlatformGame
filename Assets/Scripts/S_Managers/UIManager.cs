using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        GameEvents.OnPlayerDamaged += UpdateHealth;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerDamaged -= UpdateHealth;
    }

    void Start()
    {
        var player = FindObjectOfType<PlayerHealth>();
        if (player != null)
            healthText.text = player.currentHealth.ToString();
    }

    void UpdateHealth(int dmg, int currentHealth)
    {
        healthText.text = currentHealth.ToString();
    }
}
