using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameEvents.PlayerDamaged(amount, currentHealth);
            GameEvents.PlayerDied();
            return;
        }

        GameEvents.PlayerDamaged(amount, currentHealth);
    }
}
