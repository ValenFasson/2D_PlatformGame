using UnityEngine;

public class EnemyDamageOnTouch : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private bool onlyAffectTaggedPlayer = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (onlyAffectTaggedPlayer && !other.CompareTag(playerTag)) return;

        var health = other.GetComponent<PlayerHealth>()
            ?? other.GetComponentInParent<PlayerHealth>()
            ?? other.GetComponentInChildren<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}