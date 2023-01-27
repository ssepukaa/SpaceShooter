using UnityEngine;

namespace Assets.Scripts
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private int damage = 100;

        public int GetDamage()
        {
            return damage;
        }

        public void Hit()    
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Projectile" && gameObject.tag == "Projectile")
            {
                Hit();
            }
        
        }
    
    }
}
