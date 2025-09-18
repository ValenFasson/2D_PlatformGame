using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{


    [Header("Vidas del jugador")]
    public int maxLives = 3;
    public int currentLives;

    bool isDead = false;

    private void Awake()
    {
        currentLives = Mathf.Max(1, maxLives);
    }

    // El enemigo llama a esto cuando hay contacto
    public void TakeDamage(int amount = 1)
    {
        if (isDead) return;

        currentLives -= Mathf.Max(1, amount);
        currentLives = Mathf.Max(0, currentLives);

        if (currentLives <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // Destruye el objeto del jugador
        Destroy(gameObject);

        // Notifica al GameManager para ir a la escena de derrota
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerDied();
        }
    }
}