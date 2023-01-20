using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float padding = 0.5f;

        [Header("Projectile")]
        [SerializeField] private GameObject laserPlayer;
        [SerializeField] private float projectileSpeed = 20f;
        [SerializeField] private float projectileFiringPeriod = 0.3f;

        [Header("Stats")]
        [SerializeField] private float health = 500f;

        private Coroutine firingCoroutine;

        private float xMin;
        private float xMax;
        private float yMin;
        private float yMax;
        

        void Start()
        {
            SetUpMoveBoundaries();
        }


        // Update is called once per frame
        void Update()
        {
            Move();
            Fire();
        }

        private void Fire()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                firingCoroutine = StartCoroutine(FireContinue());
            }

            if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine(firingCoroutine);
            }
        }

        IEnumerator FireContinue()
        {
            while (true)
            {
                GameObject laser = Instantiate(laserPlayer,
                        transform.position,
                        Quaternion.identity)
                    as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                yield return new WaitForSeconds(projectileFiringPeriod);
            }
        }

        private void Move()
        {
            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
            var newPosX = Math.Clamp(transform.position.x + deltaX, xMin, xMax);
            var newPosY = Math.Clamp(transform.position.y + deltaY, yMin, yMax);
            //var newPosY = transform.position.y + deltaY;
            transform.position = new Vector2(newPosX, newPosY);
        }

        private void SetUpMoveBoundaries()
        {
            Camera gameCamera = Camera.main;
            xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
            xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).y - padding;
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
}