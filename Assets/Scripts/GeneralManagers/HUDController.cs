using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] Image boostBarImage = null;
    [SerializeField]Text shipAmmoText = null;
    [SerializeField]Text shipScoreText = null;

    [SerializeField]Image[] shieldSprites = null;

    ShipAttack shipAttackHandler;
    ScoreController shipScoreController;

    void Awake(){
        shipAttackHandler = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipAttack>();
        shipScoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
    }

    void FixedUpdate(){
        shipAmmoText.text = this.shipAttackHandler.GetShipCurrentAmmo().ToString();
        shipScoreText.text = "Score: " + this.shipScoreController.GetPlayerScore().ToString();
    }

    public void DecreaseBoostBar(float amount){
        boostBarImage.fillAmount -= amount;
    }

    public void IncreaseBoostBar(float amount){
        boostBarImage.fillAmount += amount;   
    }

    // public void RemoveOneShieldFromHud(){
    //     if(shieldSpritesCounter >= 0){
    //         this.shieldSprites[shieldSpritesCounter].enabled = false;
    //         this.shieldSpritesCounter--;
    //     }
    // }

    // public void AddOneShieldToHud(){
    //     if(shieldSpritesCounter <= 4){
    //         this.shieldSprites[shieldSpritesCounter].enabled = true;
    //         this.shieldSpritesCounter++;
    //     }
    // }

    public void UpdateShieldHUD(int playerShield){
        for(int i = 0; i < shieldSprites.Length; i++){
            if(i <= playerShield - 1){
                shieldSprites[i].enabled = true;
            }else{
                shieldSprites[i].enabled = false;
            }
        }
    }
}
