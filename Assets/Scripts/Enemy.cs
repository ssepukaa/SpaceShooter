using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private float shotCounter;
    [SerializeField] private float minTimeBetweenShots = 0.1f;
    [SerializeField] private float maxTimeBetweenShots = 5f;
    [SerializeField] private GameObject laserEnemy;
    [SerializeField] private float projectileSpeed = 10f;

    [Header("Stats")]
    [SerializeField] private float health = 100;

    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    }

   
    void Update()
    {
        CountDownAndShoot();

    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserEnemy,
                transform.position,
                Quaternion.identity)
            as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        CheckHealth();

    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
