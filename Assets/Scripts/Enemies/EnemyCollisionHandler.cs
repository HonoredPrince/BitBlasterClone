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
        }
    }

    void DestroyEnemy(string typeOfEnemy, Collider2D collision){
        switch(typeOfEnemy){
            case "Enemy1":
                shipScoreController.AddScore(10);
                enemyDropsController.DropAmmo(transform);
                Enemy1Splitter enemy1Splitter = GetComponent<Enemy1Splitter>();
                enemy1Splitter.SpawnSplittedParts();
                Destroy(this.gameObject);
                break;
            case "Enemy1_Splitted":
                shipScoreController.AddScore(20);
                enemyDropsController.DropAmmo(transform);
                Destroy(this.gameObject);
                break;
            case "Enemy2":
                shipScoreController.AddScore(20);
                enemyDropsController.DropAmmo(transform);
                Destroy(this.gameObject);
                break; 
            case "Enemy3":
                shipScoreController.AddScore(30);
                enemyDropsController.DropAmmo(transform);
                Destroy(this.gameObject);
                break; 
        }
    }
}
