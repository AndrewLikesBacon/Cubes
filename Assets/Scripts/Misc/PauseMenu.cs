using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject playerAim;
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (gameIsPaused) {

                ResumeGame();

            } else {

                PauseGame();
                
            }

        }

    }

    private void PauseGame() {

        pauseMenuUI.SetActive(true);
        playerAim.SetActive(false);
        Time.timeScale = 0;
        gameIsPaused = true;

    }

    public void ResumeGame() {

        pauseMenuUI.SetActive(false);
        playerAim.SetActive(true);
        Time.timeScale = 1;
        gameIsPaused = false;

    }
}
