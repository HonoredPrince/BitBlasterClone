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
                                enemyHit = null;
    
    /*TODO
    Ship's Berserker SFX,
    Ship's Laser SFX,
    Ship's Thrusts SFX,
    Ammunition SFX,
    Enemy 1 Hit SFX,
    Enemy 1 Splits Hit SFX,
    Enemy 2 Hit SFX,
    Enemy 3 Hit SFX,
    */ 

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
            case "enemyHit":
                SFXAudioSource.PlayOneShot(enemyHit);
                break;
            default:
                //Apenas para testes
                Debug.Log("Missing AudioClip association");
                break;
        }
    }
}
