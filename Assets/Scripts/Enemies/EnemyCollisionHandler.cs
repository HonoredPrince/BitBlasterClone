using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    EnemyDropsController enemyDropsController;

    void Awake(){
        enemyDropsController = GetComponent<EnemyDropsController>();
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
                enemyDropsController.DropAmmo(transform);
                Enemy1Splitter enemy1Splitter = GetComponent<Enemy1Splitter>();
                enemy1Splitter.SpawnSplittedParts();
                Destroy(this.gameObject);
                break;
            case "Enemy1_Splitted":
                enemyDropsController.DropAmmo(transform);
                Destroy(this.gameObject);
                break;
            case "Enemy3":
                enemyDropsController.DropAmmo(transform);
                Destroy(this.gameObject);
                break; 
        }
    }
}
