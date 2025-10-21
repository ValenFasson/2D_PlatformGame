using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public ObjectPool<Bullet> pool;
    private Transform target;
    private float speed;
    private Vector2 direction;
    private Coroutine lifetimeCoroutine;

    public void OnEnable()
    {
        lifetimeCoroutine = StartCoroutine(Lifetime(5f)); //seguridad
    }
    public void OnDisable()
    {
        if (lifetimeCoroutine != null)
            StopCoroutine(lifetimeCoroutine);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        ReleaseThis();
    }
    private IEnumerator Lifetime(float time)
    {
        yield return new WaitForSeconds(time);
        ReleaseThis();
    }
    private IEnumerator flying()
    {
        while (Vector2.Distance(transform.position, target.position) > 0.2f)
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
            yield return null;
        }

        ReleaseThis();
    }
    public void GoToTarget(Transform player, float speed)
    {
        this.target = player;
        this.speed = speed;
        direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        StartCoroutine(flying());
    }
    private void ReleaseThis() 
    {
        pool.Release(this);
    }
}
