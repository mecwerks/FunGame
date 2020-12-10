using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Damageable player;
    public Damageable enemy;
    public GameObject GameOverMenu;

    public bool GameOver { get; private set; } = false;

    protected GameManager() { } // Prevent non-singleton constructor use.

    // Update is called once per frame
    private void Update()
    {
        if (!GameOver)
        {
            if (player && player.health <= 0)
            {
                EndGame(false);
            }
            else if (enemy && enemy.health <= 0)
            {
                EndGame(true);
            }

        }
    }

    void EndGame(bool win)
    {
        Cursor.lockState = CursorLockMode.Confined;
        GameObject menu = Instantiate(GameOverMenu);
        GameOverMenu goMenu = menu.GetComponent<GameOverMenu>();

        if (goMenu)
            goMenu.SetStatus(win);

        GameOver = true;
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene.name);
    }
}
