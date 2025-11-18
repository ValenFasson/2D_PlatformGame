using System;

public static class GameEvents
{
    public static event Action<int, int> OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    public static void PlayerDamaged(int amount, int currentHealth)
    {
        OnPlayerDamaged?.Invoke(amount, currentHealth);
    }

    public static void PlayerDied()
    {
        OnPlayerDeath?.Invoke();
    }
}
