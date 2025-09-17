using UnityEngine;

public class Multiplier : MonoBehaviour
{
    bool isCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected) return;
        if (!other.CompareTag("Player")) return;

        Collect();
    }

    void Collect()
    {
        if (isCollected) return;
        isCollected = true;

        // Llamada directa al singleton ScoreManager (añade a la cola)
        ScoreManager.instance.AddMultiplier();

        // Destruir el multiplicador
        Destroy(gameObject);
    }
}