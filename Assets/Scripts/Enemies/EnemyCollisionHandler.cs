using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    EnemyDropsController enemyDropsController;
    ScoreController shipScoreController;
    SoundController soundController;
    Material dissolveMaterial;

    void Awake(){
        enemyDropsController = GetComponent<EnemyDropsController>();
        shipScoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
        dissolveMaterial = GetComponent<SpriteRenderer>().material;
    }

    void OnTriggerEnter2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Bullet1":
                soundController.playSFX("enemyBulletHit");
                DestroyEnemy(this.gameObject.tag, collision);          
                break;
            case "ShipBerserker":
                soundController.playSFX("enemyBulletHit");
                DestroyEnemy(this.gameObject.tag, collision);          
                break;
            case "Laser":
                soundController.playSFX("enemyLaserHit");
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
                StartCoroutine(DissolveEnemy());
                DropItem();
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

    IEnumerator DissolveEnemy(){
        float fadeValue = 1;
        dissolveMaterial.SetFloat("_Fade", fadeValue);
        Collider2D enemyCollider;

        //Stop enemy movement after being hit while dissolving
        switch(this.gameObject.tag){
            case "Enemy1":
                GetComponent<Enemy1Movement>().enabled = false;
                enemyCollider = GetComponent<PolygonCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy1_Splitted":
                GetComponent<Enemy1Movement>().enabled = false;
                enemyCollider = GetComponent<PolygonCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy2":
                GetComponent<Enemy1Movement>().enabled = false;
                enemyCollider = GetComponent<CircleCollider2D>();
                enemyCollider.enabled = false;
                break;
            case "Enemy3":
                GetComponent<Enemy3Movement>().enabled = false;
                enemyCollider = GetComponent<CircleCollider2D>();
                enemyCollider.enabled = false;
                break;
        }

        //Try later to get this values to play the fade dissolve animation more smoothly
        for(float i = 0f; i < 1f; i += 0.1f){
            yield return new WaitForSeconds(0.1f);
            fadeValue -= 0.3f;
            //Debug.Log(fadeValue);
            dissolveMaterial.SetFloat("_Fade", fadeValue);
        }
        Destroy(this.gameObject);
    }
}
