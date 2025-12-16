using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    public float reuseDelay = 0.5f;

    bool canUse = true;

    void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col != null)
            col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canUse)
            return;

        if (!other.CompareTag("Player"))
            return;

        UseDoor();
    }

    void UseDoor()
    {
        if (Singleton.instance == null)
            return;

        Singleton.instance.ShowRoomResults();
        canUse = false;
    }
}
