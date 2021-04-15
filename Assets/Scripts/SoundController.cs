using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource musicAudioSource = null, SFXAudioSource = null;
    
    [SerializeField] AudioClip musicOST = null;
    
    [SerializeField] AudioClip[] shipFiring = null;
    [SerializeField] AudioClip nukeDeploy = null;
    
    public void playMusic(){
        musicAudioSource.PlayOneShot(musicOST);
    }

    public void playSFX(string sfxName){
        switch(sfxName){
            case "shipFiring":
                SFXAudioSource.PlayOneShot(shipFiring[Random.Range(0, shipFiring.Length)]);
                break;
            case "nukeDeploy":
                SFXAudioSource.PlayOneShot(nukeDeploy);
                break;
        }
    }
}
