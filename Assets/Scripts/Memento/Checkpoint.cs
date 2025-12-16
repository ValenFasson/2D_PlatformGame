using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Color activatedColor = Color.white;
    private bool used;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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

        if (spriteRenderer != null)
        {
            spriteRenderer.color = activatedColor;
        }

        if (GameMementoManager.Instance != null)
        {
            GameMementoManager.Instance.SaveCheckpoint();
        }
    }
}
