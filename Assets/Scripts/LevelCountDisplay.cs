using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class LevelCountDisplay : MonoBehaviour
{
    private TextMeshProUGUI textLevelCount;
    private GameSession gameSession;
    void Start()
    {
        textLevelCount = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        if(!gameSession) return;
        textLevelCount.text = gameSession.GetLevelCount().ToString();
    }
}
