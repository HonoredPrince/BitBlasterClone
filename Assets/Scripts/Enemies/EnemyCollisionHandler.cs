using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    EnemyDropsController enemyDropsController;
    ScoreController shipScoreController;

    void Awake(){
        enemyDropsController = GetComponent<EnemyDropsController>();
        shipScoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
    }

    void OnTriggerEnter2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Bullet1":
                DestroyEnemy(this.gameObject.tag, collision);          
                break;
            case "ShipBerserker":
                DestroyEnemy(this.gameObject.tag, collision);          
                break;
            case "Laser":
                DestroyEnemy(this.gameObject.tag, collision);          
                break;
        }
    }

    void DestroyEnemy(string typeOfEnemy, Collider2D collision){
        switch(typeOfEnemy){
            case "Enemy1":
                shipScoreController.AddScore(10);
                DropItem();
                Enemy1Splitter enemy1Splitter = GetComponent<Enemy1Splitter>();
                enemy1Splitter.SpawnSplittedParts();
                Destroy(this.gameObject);
                break;
            case "Enemy1_Splitted":
                shipScoreController.AddScore(20);
                DropItem();
                Destroy(this.gameObject);
                break;
            case "Enemy2":
                shipScoreController.AddScore(20);
                DropItem();
                Destroy(this.gameObject);
                break; 
            case "Enemy3":
                shipScoreController.AddScore(30);
                DropItem();
                Destroy(this.gameObject);
                break; 
        }
    }

    public void DropItem(){
        //TODO: Find a better way to implement the % chance of every item dropped by enemies
        float chance = Random.Range(0f, 100f);
        if(chance <= 1f){
            enemyDropsController.DropNukePowerUp(transform);
        }else if(chance <= 2f){
            enemyDropsController.DropBerserkerPowerUp(transform);
        }else if(chance <= 3f){
            enemyDropsController.DropLaserPowerUp(transform);
        }else if(chance <= 5f){
            enemyDropsController.DropShieldPowerUp(transform);
        }else if(chance <= 10f){
            enemyDropsController.DropTripleBulletPowerUp(transform);
        }else{
            enemyDropsController.DropAmmo(transform);
        }
    }
}
