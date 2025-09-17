using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Valor")]
    public int coinValue = 10;              // Valor base de la moneda

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

        // Llamada directa al singleton ScoreManager
        ScoreManager.instance.AddCoinValue(coinValue);

        // Destruir la moneda
        Destroy(gameObject);
    }
}