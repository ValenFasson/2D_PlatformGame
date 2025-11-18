using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;
    public float cooldown = 0.5f;
    float timer;

    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (timer > 0) return;
        if (!other.CompareTag("Player")) return;

        var health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
            timer = cooldown;
        }
    }
}
