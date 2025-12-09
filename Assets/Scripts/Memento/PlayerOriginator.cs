using UnityEngine;

public class PlayerOriginator : MonoBehaviour
{
    private PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public PlayerMemento SaveState()
    {
        return new PlayerMemento(transform.position, playerHealth.currentHealth);
    }

    public void RestoreState(PlayerMemento memento)
    {
        transform.position = memento.Position;
        playerHealth.currentHealth = memento.Health;

        GameEvents.PlayerDamaged(0, playerHealth.currentHealth);
    }
}
