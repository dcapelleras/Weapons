using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Referencias
    [SerializeField] GameObject PauseMenu;

    //Variables de estado
    bool gamePaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void UnpauseGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }

    private void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
