using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class RecordScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI textRecordScore;
    private GameSession gameSession;
    void Start()
    {
        textRecordScore = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

   
    void Update()
    {
        textRecordScore.text = gameSession.GetRecordScore().ToString();
    }
}
