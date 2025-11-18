using System;

public static class GameEvents
{
    public static event Action<int, int> OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    public static event Action<bool> OnShieldStateChanged;
    public static event Action<bool> OnSpeedStateChanged;

    public static void PlayerDamaged(int amount, int currentHealth)
    {
        OnPlayerDamaged?.Invoke(amount, currentHealth);
    }

    public static void PlayerDied()
    {
        OnPlayerDeath?.Invoke();
    }

    public static void ShieldStateChanged(bool active)
    {
        OnShieldStateChanged?.Invoke(active);
    }

    public static void SpeedStateChanged(bool active)
    {
        OnSpeedStateChanged?.Invoke(active);
    }
}
