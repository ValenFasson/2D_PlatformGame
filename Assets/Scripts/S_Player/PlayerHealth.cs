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
        GameEvents.PlayerDamaged(amount, currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameEvents.PlayerDied();
        }
    }
}
