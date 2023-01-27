using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [Header("Projectile")]
        private float shotCounter;
        [SerializeField] private float minTimeBetweenShots = 0.1f;
        [SerializeField] private float maxTimeBetweenShots = 4f;
        [SerializeField] private GameObject laserEnemy;
        [SerializeField] private float projectileSpeed = 10f;

        [Header("Stats")]
        [SerializeField] private int health = 100;

        [SerializeField] private int scoreValue = 150;


        [Header("SFX/VFX")]
        [SerializeField] private GameObject deathVFX;
        [SerializeField] private float durationExplosion =1f;
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip fireSound;
        [SerializeField] [Range(0, 1)] private float deathSoundVolume = 0.75f;
        [SerializeField] [Range(0, 1)] private float fireSoundVolume = 0.75f;

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
            AudioSource.PlayClipAtPoint(fireSound,laser.transform.position, fireSoundVolume);

        }
    

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if(!damageDealer){return;}
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
            FindObjectOfType<GameSession>().AddToScore(scoreValue);  
            Destroy(gameObject);
            GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explosion, durationExplosion);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);

        }
    }
}
