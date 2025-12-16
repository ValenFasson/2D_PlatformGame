using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool used;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (used)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        used = true;

        if (GameMementoManager.Instance != null)
        {
            GameMementoManager.Instance.SaveCheckpoint();
        }
    }
}
