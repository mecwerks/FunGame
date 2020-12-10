using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public TMPro.TextMeshProUGUI statusText;

    GameManager manager;

    private void Start()
    {
        manager = FindObjectsOfType<GameManager>()[0];
    }

    public void SetStatus(bool win)
    {
        statusText.text = win ? "You Win!" : "Game Over";
    }

    public void OnRetry()
    {
        manager.RestartGame();
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
