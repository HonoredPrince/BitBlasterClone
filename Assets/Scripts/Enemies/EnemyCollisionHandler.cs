using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Bullet1":
                DestroyEnemy(this.gameObject.tag, collision);
                // if(this.gameObject.tag == "Enemy1"){
                //     DestroyEnemy("Enemy1", collision);
                // }else if(this.gameObject.tag == "Enemy1_Splitted"){
                //     DestroyEnemy("Enemy1SplittedPart", collision);
                // }                
                break;
        }
    }

    void DestroyEnemy(string typeOfEnemy, Collider2D collision){
        switch(typeOfEnemy){
            case "Enemy1":
                Enemy1Splitter enemy1Splitter = GetComponent<Enemy1Splitter>();
                enemy1Splitter.SpawnSplittedParts();
                Destroy(this.gameObject);
                break;
            case "Enemy1_Splitted":
                Destroy(this.gameObject);
                break;
            case "Enemy3":
                Destroy(this.gameObject);
                break;
        }
    }
}
