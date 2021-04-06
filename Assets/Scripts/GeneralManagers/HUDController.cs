using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] Image boostBarImage = null;
    [SerializeField] Text shipAmmoText = null;
    [SerializeField] Text shipScoreText = null;
    [SerializeField] Sprite[] bulletsTypesSprites = null;
    [SerializeField] Image bulletTypeSprite = null;

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

    public void UpdateShieldHUD(int playerShield){
        for(int i = 0; i < shieldSprites.Length; i++){
            if(i <= playerShield - 1){
                shieldSprites[i].enabled = true;
            }else{
                shieldSprites[i].enabled = false;
            }
        }
    }

    public void SetBulletTypeSprite(string bulletType){
        switch(bulletType){
            case "defaultBullet":
                this.bulletTypeSprite.sprite = bulletsTypesSprites[0];
                break;
            case "tripleBullet":
                this.bulletTypeSprite.sprite = bulletsTypesSprites[1];
                break;
        }
        
    }
}
