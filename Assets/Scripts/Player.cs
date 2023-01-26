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
        [SerializeField] private int health = 500;

        [Header("SFX?VFX")]
        [SerializeField] private GameObject deathVFX;
        [SerializeField][Range(0, 1)] private float deathSoundVolume = 0.75f;
        [SerializeField][Range(0, 1)] private float fireSoundVolume = 0.75f;

        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip fireSound;

        private Coroutine firingCoroutine;

        private float xMin;
        private float xMax;
        private float yMin;
        private float yMax;
        [SerializeField] private float durationDeathVFX = 1f;


        void Start()
        {
            SetUpMoveBoundaries();
            Fire();
        }


        // Update is called once per frame
        void Update()
        {
            Move();
            //Fire();
        }

        private void Fire()
        {
            StartCoroutine(FireContinue());
            // if (Input.GetButtonDown("Fire1"))
            // {
            //     firingCoroutine = StartCoroutine(FireContinue());
            // }
            //
            // if (Input.GetButtonUp("Fire1"))
            // {
            //     StopCoroutine(firingCoroutine);
            // }
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
                AudioSource.PlayClipAtPoint(fireSound,laser.transform.position, fireSoundVolume);
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
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
        }

        private void ProcessHit(DamageDealer damageDealer)
        {
            health -= damageDealer.GetDamage();
            damageDealer.Hit();
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {

            FindObjectOfType<Level>().LoadGameOver();
            Destroy(gameObject);
            GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explosion, durationDeathVFX);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
            

        }

        public int GetHealth()
        {
            if (health <= 0)
            {
                health = 0;
            }
            return health;
        }

       
    }
}