using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField]GameObject pauseMenuCanvas = null;
    [SerializeField]LevelLoader levelLoaderController = null;
    [SerializeField] GameObject optionsCanvas = null;
    [SerializeField] Canvas HUDCanvas = null;
    [SerializeField] AudioMixer musicAudioMixer = null, sfxAudioMixer = null;
    Resolution[] resolutions;
    [SerializeField] TMP_Dropdown resolutionDropdown = null;

    void Awake(){
        optionsCanvas.SetActive(false);
        ResolutionInitialConfiguration();
    }

    void Update (){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }

        if(pauseMenuCanvas.activeSelf || optionsCanvas.activeSelf){
            gameIsPaused = true;
        }else{
            gameIsPaused = false;
        }

    }

    void Pause(){
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OpenOptionsMenu(){
        pauseMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
        HUDCanvas.enabled = false;
    }

    public void BackOptionButton(){
        pauseMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
        HUDCanvas.enabled = true;
    }

    public void Resume(){
        pauseMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        HUDCanvas.enabled = true;
        Time.timeScale = 1f;
    }

    public void LoadMenu(){
        Time.timeScale = 1f;
        levelLoaderController.LoadLevelWithName("StartMenu");
    }

    public void SetMusicVolume(float volume){
        if(volume == -80f){
            musicAudioMixer.SetFloat("volume", volume);
        }else if(volume == 0f){
            musicAudioMixer.SetFloat("volume", volume);
        }else{
            musicAudioMixer.SetFloat("volume", volume / 2f);
        }
    }

    public void SetSFXVolume(float volume){
        if(volume == -80f){
            sfxAudioMixer.SetFloat("volume", volume);
        }else if(volume == 0f){
            sfxAudioMixer.SetFloat("volume", volume);
        }else{
            sfxAudioMixer.SetFloat("volume", volume / 2f);
        }
    }

    public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
        //Debug.Log(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    void ResolutionInitialConfiguration(){
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
}



