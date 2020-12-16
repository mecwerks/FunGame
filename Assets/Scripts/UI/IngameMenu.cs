using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenu : MonoBehaviour
{
    public Slider playerHealthSlider;
    
    Damageable player;

    void Update()
    {
        if (player)
        {
            playerHealthSlider.value = player.health / player.maxHealth;
        }
    }

    public void AssignLocalPlayer(Damageable localPlayer)
    {
        player = localPlayer;
    }
}
