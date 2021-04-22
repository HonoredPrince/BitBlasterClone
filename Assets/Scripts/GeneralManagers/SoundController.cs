using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource musicAudioSource = null, SFXAudioSource = null;
    
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
}
