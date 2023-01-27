using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class ScoreDisplay : MonoBehaviour
    {
        private TextMeshProUGUI scoreText;
        private GameSession gameSession;
        void Start()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
            gameSession = FindObjectOfType<GameSession>();
        }

        void Update()
        {
            scoreText.text = gameSession.GetScore().ToString();
        }
    }
}
