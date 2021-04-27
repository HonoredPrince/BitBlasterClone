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
                //StartCoroutine(Resume());
                Resume();
            }else{
                //StartCoroutine(Pause());
                Pause();
            }
        }
    }

    void Pause(){
        pauseMenuCanvas.SetActive(true);
        //yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume(){
        pauseMenuCanvas.SetActive(false);
        //yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        gameIsPaused = false;
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



