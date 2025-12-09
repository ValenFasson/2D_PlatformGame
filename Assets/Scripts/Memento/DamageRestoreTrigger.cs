using UnityEngine;

public class DamageRestoreTrigger : MonoBehaviour
{
    void OnEnable()
    {
        GameEvents.OnPlayerDamaged += OnDamaged;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerDamaged -= OnDamaged;
    }

    void OnDamaged(int dmg, int currentHealth)
    {
        if (dmg == 0)
        {
            return;
        }

        if (GameMementoManager.Instance == null)
        {
            return;
        }

        if (GameMementoManager.Instance.IsRestoring)
        {
            return;
        }

        if (!GameMementoManager.Instance.HasMemento)
        {
            return;
        }

        GameMementoManager.Instance.RestoreFromDamage();
    }
}
