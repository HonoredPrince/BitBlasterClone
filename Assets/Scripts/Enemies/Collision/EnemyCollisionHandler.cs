using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCollisionHandler : MonoBehaviour
{
    EnemyHealthManager enemyHealthManager;
    SoundController soundController;
    ShipSelectorController shipSelectorController;

    public bool hasPassedBorders;
    bool hasBeenHitByBerserker = false;

    void Awake(){
        try{
            shipSelectorController = GameObject.FindGameObjectWithTag("ShipSelectorController").GetComponent<ShipSelectorController>();
        }catch (Exception e){
            Debug.Log("Missing ship selector object \n" + e.Message);
        }

        enemyHealthManager = GetComponent<EnemyHealthManager>();
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
        
        if(this.gameObject.tag == "Enemy1_Splitted"){
            this.hasPassedBorders = true;
        }else{
            this.hasPassedBorders = false;
        }
    }

    
    void OnTriggerEnter2D(Collider2D collision){
        //TODO: Find new SFXs for the new bullet types!
        switch(collision.gameObject.tag){
            case "Bullet1":
                soundController.playSFX("enemyBulletHit");
                enemyHealthManager.SetDissolveColor(new Vector4(4, 190, 191, 0));   
                enemyHealthManager.EnemyHit(1);       
                break;
            case "PassthroughBullet":
                soundController.playSFX("enemyBulletHit");
                enemyHealthManager.SetDissolveColor(new Vector4(4, 190, 4, 0)); 
                enemyHealthManager.EnemyHit(1);          
                break;
            case "BouncerBullet":
                soundController.playSFX("enemyBulletHit");
                enemyHealthManager.SetDissolveColor(new Vector4(254, 254, 73, 255));
                enemyHealthManager.EnemyHit(1);           
                break;
            case "PurpleBombExplosionRadius":
                enemyHealthManager.SetDissolveColor(new Vector4(98, 26, 142, 255)); 
                enemyHealthManager.EnemyHit(3);          
                break;
            case "CombinedShot":
                soundController.playSFX("enemyBulletHit");
                enemyHealthManager.SetDissolveColor(new Vector4(252, 169, 3, 255)); 
                enemyHealthManager.EnemyHit(1);          
                break;
            case "ShipBerserker":
                if(!hasBeenHitByBerserker){
                    hasBeenHitByBerserker = true;
                    soundController.playSFX("enemyBulletHit");
                    BerserkerDissolveColorHelper();
                    enemyHealthManager.EnemyHit(3);
                }        
                break;
            case "Laser":
                soundController.playSFX("enemyLaserHit");
                enemyHealthManager.EnemyHit(2); 
                enemyHealthManager.SetDissolveColor(new Vector4(254, 95, 75, 0));          
                break;
        }
    }

    void BerserkerDissolveColorHelper(){
        switch(shipSelectorController.currentShipTypeIndex){
            case 0:
                enemyHealthManager.SetDissolveColor(new Vector4(4, 190, 191, 0));
                break;
            case 1:
                enemyHealthManager.SetDissolveColor(new Vector4(4, 190, 4, 0));
                break;
            case 2:
                enemyHealthManager.SetDissolveColor(new Vector4(252, 169, 3, 255));
                break;
            case 3:
                enemyHealthManager.SetDissolveColor(new Vector4(98, 26, 142, 255));
                break;
        }
    }
    
    void OnTriggerExit2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "BordersPoints":
                this.hasPassedBorders = true;
                break;
        }
    }

    public void DisableEnemyCollision(){
        //For stopping the enemy collision after being hit   
        Collider2D enemyCollider;

        switch(this.gameObject.tag){
            case "Enemy1":
                enemyCollider = GetComponent<PolygonCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy1_Splitted":
                enemyCollider = GetComponent<PolygonCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy2":
                enemyCollider = GetComponent<CircleCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy3":
                enemyCollider = GetComponent<CircleCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy4":
                enemyCollider = GetComponent<CapsuleCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy5":
                enemyCollider = GetComponent<CircleCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy6":
                enemyCollider = GetComponent<CircleCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy7":
                enemyCollider = GetComponent<PolygonCollider2D>();
                enemyCollider.enabled = false;
                break;
        }
    }

}