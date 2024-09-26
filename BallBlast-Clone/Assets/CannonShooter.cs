using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooter : MonoBehaviour
{
    public GameObject bullet;
    public CannonScript cannonScript;
    public Transform bulletSpawnPoint;
    public PlayerStats playerStats;

    private float lastShotTime;

    private void Awake() 
    {
        cannonScript = GetComponent<CannonScript>();
        playerStats = GetComponent<PlayerStats>();
        bullet = Resources.Load<GameObject>("bullet");
    }

    // Start is called before the first frame update
    void Start()
    {
        lastShotTime = -playerStats.bulletInterval; // Permitir disparo imediato no início
    }

    // Update is called once per frame
    void Update()
    {
        if (cannonScript.isDragging && Time.time >= lastShotTime + playerStats.bulletInterval)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < playerStats.bulletsPerShot; i++)
        {
            GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
            rb.velocity = bulletSpawnPoint.up * playerStats.bulletSpeed;
        }
    }
}
