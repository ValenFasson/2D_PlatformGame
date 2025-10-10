using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    private ObjectPool<Bullet> bulletPool;
    [SerializeField] private Bullet projectilePrefab;

    public void Awake()
    {
        bulletPool = new ObjectPool<Bullet> (Create, Get, Release, Delete, false, 8 , 10);
    }

    public Bullet Create()
    {
        Bullet bullet = Instantiate (projectilePrefab);
        bullet.gameObject.SetActive (false);
        bullet.pool = bulletPool;
        return bullet;
    }
    public void Get(Bullet bullet) 
    {
        bullet.gameObject.SetActive(true);
    }
    public void Release(Bullet bullet) 
    {
        bullet.gameObject.SetActive(false);
    }
    public void Delete(Bullet bullet) 
    {
        Destroy(bullet.gameObject);
    }

    public Bullet GetBullet() 
    {
        return bulletPool.Get();
    }
}
