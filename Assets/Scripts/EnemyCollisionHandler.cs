using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    Enemy1Controller enemy1Controller;

    void Awake(){
        enemy1Controller = GetComponent<Enemy1Controller>();
    }

    void OnTriggerEnter2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Bullet1":
                if(this.gameObject.tag == "Enemy1"){
                    DestroyEnemy("Enemy1", collision);
                }else if(this.gameObject.tag == "Enemy1_Splitted"){
                    DestroyEnemy("Enemy1SplittedPart", collision);
                }                
                break;
        }
    }

    void DestroyEnemy(string typeOfEnemy, Collider2D collision){
        switch(typeOfEnemy){
            case "Enemy1":
                enemy1Controller.SpawnSplittedParts();
                Destroy(this.gameObject);
                break;
            case "Enemy1SplittedPart":
                Destroy(this.gameObject);
                break;
        }
    }
}
