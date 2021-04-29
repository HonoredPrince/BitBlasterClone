using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource musicAudioSource = null, SFXAudioSource = null;
    
    [SerializeField] AudioSource[] SFXAudioSourceGroup = null, musicAudioSourceGroup = null;
    
    [SerializeField] float musicGroupVolume = 0;
    [SerializeField] float sfxGroupVolume = 0;

    [SerializeField] AudioClip musicOST = null;
    
    [SerializeField] AudioClip[] shipFiring = null;
    [SerializeField] AudioClip  nukeDeploy = null, 
                                shipBoost = null,
                                shipBoostRecharge = null,
                                ammoPickup = null,
                                tripleBulletPowerUpPickup = null,
                                shieldPowerUpPickup = null,
                                laserPowerUpPickup = null,
                                shipBerserkerPowerUpPickup = null,
                                nukePowerUpPickup = null,
                                shipHitDamage = null,
                                shipDeath = null,
                                enemyHit = null,
                                enemyLaserHit = null;

    
    void Update(){
        //TODO: Handle volume adjustments when game is paused
        UdpateAudioSourceGroupsVolumes();
        ManageSFXAudioSourceGroupsState(PauseMenuManager.gameIsPaused);

        if(PauseMenuManager.gameIsPaused){
            musicGroupVolume = 0.01f;
            sfxGroupVolume = 0.1f;
            
        }else{
            musicGroupVolume = 0.05f;
            sfxGroupVolume = 0.5f;
        }
    }
    
    public void playMusic(){
        musicAudioSource.PlayOneShot(musicOST);
    }

    public void playSFX(string sfxName){
        int randomLaserSound;
        
        switch(sfxName){
            case "shipFiring":
                randomLaserSound = Random.Range(0, shipFiring.Length);
                SFXAudioSource.PlayOneShot(shipFiring[Random.Range(0, shipFiring.Length)]);
                break;
             case "shipTripleFiring":
                randomLaserSound = Random.Range(0, shipFiring.Length);
                SFXAudioSource.PlayOneShot(shipFiring[randomLaserSound]);
                SFXAudioSource.PlayOneShot(shipFiring[randomLaserSound]);
                break;
            case "nukeDeploy":
                SFXAudioSource.PlayOneShot(nukeDeploy);
                break;
            case "shipBoost":
                SFXAudioSource.PlayOneShot(shipBoost);
                break;
            case "shipBoostRecharge":
                SFXAudioSource.PlayOneShot(shipBoostRecharge);
                break;
            case "ammoPickup":
                SFXAudioSource.PlayOneShot(ammoPickup);
                break;
            case "shieldPowerUpPickup":
                SFXAudioSource.PlayOneShot(shieldPowerUpPickup);
                break;
            case "tripleBulletPowerUpPickup":
                SFXAudioSource.PlayOneShot(tripleBulletPowerUpPickup);
                break;
            case "berserkerPowerUpPickup":
                SFXAudioSource.PlayOneShot(shipBerserkerPowerUpPickup);
                break;
            case "laserPowerUpPickup":
                SFXAudioSource.PlayOneShot(laserPowerUpPickup);
                break;
            case "nukePowerUpPickup":
                SFXAudioSource.PlayOneShot(nukePowerUpPickup);
                break;
            case "shipHitDamage":
                SFXAudioSource.PlayOneShot(shipHitDamage);
                break;
            case "shipDeath":
                SFXAudioSource.PlayOneShot(shipDeath);
                break;
            case "enemyBulletHit":
                SFXAudioSource.PlayOneShot(enemyHit);
                break;
            case "enemyBerserkerHit":
                SFXAudioSource.PlayOneShot(enemyHit);
                break;
            case "enemyLaserHit":
                SFXAudioSource.PlayOneShot(enemyLaserHit);
                break;
            default:
                //Only for tests purposes
                Debug.Log("Missing AudioClip association");
                break;
        }
    }

    void UdpateAudioSourceGroupsVolumes(){
        for(int i = 0; i < musicAudioSourceGroup.Length; i++){
            if(musicAudioSourceGroup[i].volume != musicGroupVolume){
                musicAudioSourceGroup[i].volume = musicGroupVolume;
                //Debug.Log("Music Audio Source Group Changed");
            }
        }
        for(int i = 0; i < SFXAudioSourceGroup.Length; i++){
            if(SFXAudioSourceGroup[i].volume != sfxGroupVolume){
                SFXAudioSourceGroup[i].volume = sfxGroupVolume;
                //Debug.Log("SFX Audio Source Group Changed");
            }
        }
    }

    void ManageMusicAudioSourceGroupsState(bool isGamePaused){
        for(int i = 0; i < musicAudioSourceGroup.Length; i++){
            if(isGamePaused){
                musicAudioSourceGroup[i].Pause();
                //Debug.Log("Music Audio Source Group Paused");
            }else{
                musicAudioSourceGroup[i].UnPause();
                //Debug.Log("Music Audio Source Group Unpaused");
            }
        }
    }

    void ManageSFXAudioSourceGroupsState(bool isGamePaused){
        for(int i = 0; i < SFXAudioSourceGroup.Length; i++){
            if(isGamePaused){
                SFXAudioSourceGroup[i].Pause();
                //Debug.Log("SFX Audio Source Group Paused");
            }else{
                SFXAudioSourceGroup[i].UnPause();
                //Debug.Log("SFX Audio Source Group Unpaused");
            }
        }
    }

}
