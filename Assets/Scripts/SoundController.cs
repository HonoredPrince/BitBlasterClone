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
                                shieldPowerUpPickup = null,
                                nukePowerUpPickup = null,
                                shipHitDamage = null;
    
    /*TODO
    Ship's death SFX,
    Ship's PowerUps SFX,
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
        switch(sfxName){
            case "shipFiring":
                SFXAudioSource.PlayOneShot(shipFiring[Random.Range(0, shipFiring.Length)]);
                break;
            case "nukeDeploy":
                SFXAudioSource.PlayOneShot(nukeDeploy);
                break;
            case "shipBoost":
                SFXAudioSource.PlayOneShot(shipBoost);
                break;
            case "shieldPowerUpPickup":
                SFXAudioSource.PlayOneShot(shieldPowerUpPickup);
                break;
            case "nukePowerUpPickup":
                SFXAudioSource.PlayOneShot(nukePowerUpPickup);
                break;
            case "shipHitDamage":
                SFXAudioSource.PlayOneShot(shipHitDamage);
                break;
            default:
                //Apenas para testes
                Debug.Log("Missing AudioClip association");
                break;
        }
    }
}
