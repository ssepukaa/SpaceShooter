using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class HealthDisplay : MonoBehaviour
    {
        private TextMeshProUGUI healthText;
        private Player player;
        void Start()
        {
            healthText = GetComponent<TextMeshProUGUI>();
            player = FindObjectOfType<Player>();
        }

        void Update()
        {
            healthText.text = player.GetHealth().ToString();
        }
    }
}
