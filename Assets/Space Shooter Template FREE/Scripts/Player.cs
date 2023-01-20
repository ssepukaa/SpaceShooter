using UnityEngine;

namespace Assets.Space_Shooter_Template_FREE.Scripts
{
    /// <summary>
    /// This script defines which sprite the 'Player" uses and its health.
    /// </summary>
    public class Player : MonoBehaviour
    {
        public GameObject destructionFX;

        public static Player instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        //method for damage proceccing by 'Player'
        public void GetDamage(int damage)
        {
            Destruction();
        }

        //'Player's' destruction procedure
        void Destruction()
        {
            Instantiate(destructionFX, transform.position,
                Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
            Destroy(gameObject);
        }
    }
}