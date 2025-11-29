using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    [Header("Door")]
    public float reuseDelay = 0.5f;

    bool canUse = true;

    void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canUse)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        UseDoor();
    }

    void UseDoor()
    {
        var sceneManager = FindObjectOfType<SceneStackManager>();
        if (sceneManager == null)
        {
            return;
        }
        else 
        {
            sceneManager.GetScene(); //Reemplazamos el codigo anterior y ahora sceneStackManager se encarga de procesar las escenas
        }
            StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        canUse = false;
        yield return new WaitForSeconds(reuseDelay);
        canUse = true;
    }
}
