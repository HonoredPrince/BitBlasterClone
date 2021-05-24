using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] AudioMixer musicAudioMixer = null, sfxAudioMixer = null;
    
    [SerializeField] Canvas mainCanvas = null;
    [SerializeField] Canvas optionsCanvas = null;
    
    Resolution[] resolutions;
    [SerializeField] TMP_Dropdown resolutionDropdown = null;
    
    //Initial tests on the main title canvas system implementation
    void Awake(){
        mainCanvas.enabled = true;
        optionsCanvas.enabled = false;

        SetMusicVolume(0f);
        SetSFXVolume(0f);

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
    
    public void OpenOptionsMenu(){
        mainCanvas.enabled = false;
        optionsCanvas.enabled = true;
    }

    public void BackOptionButton(){
        mainCanvas.enabled = true;
        optionsCanvas.enabled = false;
    }

    public void QuitGame(){
        Application.Quit();
    }
}
