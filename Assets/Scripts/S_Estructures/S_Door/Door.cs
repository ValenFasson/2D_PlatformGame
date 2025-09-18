using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    [Header("Door")]
    public bool goBack = false;          // true = vuelve a la escena anterior, false = va a una aleatoria siguiente
    public float reuseDelay = 0.5f;      // evita múltiples cambios por el mismo contacto

    bool canUse = true;

    void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true; // la puerta funciona por trigger
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canUse) return;
        if (!other.CompareTag("Player")) return;

        UseDoor();
    }

    void UseDoor()
    {
        var manager = FindObjectOfType<SceneStackManager>();
        if (manager == null) return;

        manager.GoToRandomNext();
        manager.roomsCompleted();
   
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        canUse = false;
        yield return new WaitForSeconds(reuseDelay);
        canUse = true;
    }
}