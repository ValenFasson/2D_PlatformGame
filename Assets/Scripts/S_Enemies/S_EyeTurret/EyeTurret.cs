using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EyeTurret : MonoBehaviour
{
    private ScriptableObject data;
    [SerializeField] private Transform player;
    [SerializeField] private Transform Point; // el punto donde salen las balas
    [SerializeField] private int rotationSpeed;
    [SerializeField] private int speed;
    [SerializeField] private float cooldown;
    private float cooldownCounter;
    [SerializeField] public BulletPool bulletPool;
    private Quaternion initialRotation;
    private RaycastHit2D raycast;
    bool isSeeingPlayer;
    public void Awake()
    {
        initialRotation = transform.rotation;
    }
    public void Update()
    {
        ShootRay();
    }

    public void AimPlayer(bool isSeeingPlayer) 
    {
        cooldownCounter += Time.deltaTime;   
        if (isSeeingPlayer)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle); // -90 si tu sprite mira hacia arriba por defecto
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (cooldownCounter >= cooldown) 
            {
                ShootBullet();
                cooldownCounter = 0;
            }
        }
        else 
        {
            cooldownCounter = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime); //back to normal position
        }
    }

    private void ShootRay()
    {
        Vector2 direction = (player.position - Point.position).normalized;
        raycast = Physics2D.Raycast(Point.position, direction);
        if (raycast.collider.tag != "Player")
        {
            isSeeingPlayer = false;
            AimPlayer(isSeeingPlayer);
            Debug.DrawRay(Point.position, direction * 5f, Color.red);
        }
        else 
        { 
            isSeeingPlayer = true;
            AimPlayer(isSeeingPlayer); 
            Debug.DrawRay(Point.position, direction * 5f, Color.green); 
        }
    }

    private void ShootBullet() 
    {
        Bullet bullet = bulletPool.GetBullet();
        bullet.transform.position = Point.position;
        bullet.GoToTarget(player,speed);
    }
}
