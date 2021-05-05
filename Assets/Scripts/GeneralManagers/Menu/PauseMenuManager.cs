using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField]GameObject pauseMenuCanvas = null;
    [SerializeField]LevelLoader levelLoaderController = null;

    void Update (){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }

        if(pauseMenuCanvas.activeSelf){
            gameIsPaused = true;
        }else{
            gameIsPaused = false;
        }

    }

    void Pause(){
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume(){
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMenu(){
        Time.timeScale = 1f;
        levelLoaderController.LoadLevelWithName("StartMenu");
    }

    public void QuitGame(){
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}



