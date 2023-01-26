using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
