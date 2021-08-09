using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    #region Private Serialized Variables
    [SerializeField] int enemyHealth = 1;
    [SerializeField] Material flashMaterial = null;
    [SerializeField] float durationOfTheFlash = 0;
    #endregion

    #region Private Variables
    SpriteRenderer enemySpriteRenderer;
    Coroutine flashRoutine;
    EnemyCollisionHandler enemyCollisionHandler;
    EnemyDropsController enemyDropsController;
    ScoreController shipScoreController;
    Material dissolveMaterial;
    float fadeValue;
    bool isDissolving;
    bool isEnemyInDamagedState = false;
    #endregion
    

    

    void Awake(){
        enemyCollisionHandler = GetComponent<EnemyCollisionHandler>();
        enemyDropsController = GetComponent<EnemyDropsController>();
        shipScoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();

        dissolveMaterial = GetComponent<SpriteRenderer>().material;
        fadeValue = 1f;
        isDissolving = false;
    }

    void Update(){
        CheckIfEnemyIsAlive();
        CheckDissolvingEnemy();
    }

    void CheckDissolvingEnemy(){
        if(isDissolving){
            fadeValue -= Time.deltaTime * 2.5f;

            if(fadeValue <= 0f){
                EnemyPostHitEvents(this.gameObject.tag);
                DropItem();
                Destroy(this.gameObject);
            }      
            
            dissolveMaterial.SetFloat("_Fade", fadeValue);
        }
    }

    void CheckIfEnemyIsAlive(){
        if(enemyHealth <= 0){
            isDissolving = true;
            enemyCollisionHandler.DisableEnemyCollision();
        }
    }

    public void SetDissolveColor(Vector4 color){
        float intensity = (color.x + color.y + color.z + color.w) / 4f;
        float factor = 2f / intensity;
        //float intensity = 0.02f;
        //float factor = Mathf.Pow(2, intensity);

        //Vector4 hdrColor = new Vector4(color.x*intensity, color.y*intensity, color.z*intensity, color.w*intensity);
        Vector4 hdrColor = new Vector4(color.x*factor, color.y*factor, color.z*factor, color.w*factor);
        this.dissolveMaterial.SetColor("_Color", hdrColor);
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
        }else if(chance <= 4f){
            enemyDropsController.DropPurpleBombPowerUp(transform);   
        }else if(chance <= 5f){
            enemyDropsController.DropShieldPowerUp(transform);
        }else if(chance <= 10f){
            enemyDropsController.DropTripleBulletPowerUp(transform);
        }else{
            enemyDropsController.DropAmmo(transform);
        }
    }

    public void EnemyPostHitEvents(string typeOfEnemy){
        switch(typeOfEnemy){
            case "Enemy1":
                shipScoreController.AddScore(10);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 10);
                Enemy1Splitter enemy1Splitter = GetComponent<Enemy1Splitter>();
                enemy1Splitter.SpawnSplittedParts();
                break;
            case "Enemy1_Splitted":
                shipScoreController.AddScore(20);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 20);
                break;
            case "Enemy2":
                shipScoreController.AddScore(20);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 20);
                break; 
            case "Enemy3":
                shipScoreController.AddScore(30);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 30);
                break; 
            case "Enemy4":
                shipScoreController.AddScore(25);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 25);
                break; 
            case "Enemy5":
                shipScoreController.AddScore(100);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 100);
                break;
            case "Enemy6":
                shipScoreController.AddScore(90);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 90);
                break;
            case "Enemy7":
                shipScoreController.AddScore(110);
                shipScoreController.SpawnScorePopUpText(this.transform.position, 110);
                break;
        }
    }

    public int GetCurrentHealth(){
        return enemyHealth;
    }
    
    public void EnemyHit(int amount){
        if(!isEnemyInDamagedState){
            if(enemyHealth > 1){
                EnemyHitFlash(amount);
            }else{
                enemyHealth -= amount;
            }
        }
    }

    public void EnemyHitFlash(int amount){
        enemyHealth -= amount;
        if(flashRoutine != null){
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(EnemyHitSpriteFlash());
    }

    IEnumerator EnemyHitSpriteFlash(){
        isEnemyInDamagedState = true;
        enemySpriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(durationOfTheFlash);

        enemySpriteRenderer.material = dissolveMaterial;
        isEnemyInDamagedState = false;

        flashRoutine = null;
    }

    public void EnemyHitBlink(int amount){
        enemyHealth -= amount;
        StartCoroutine(EnemyHitSpriteBlink());
    }

    IEnumerator EnemyHitSpriteBlink(){
        if(this.enemyHealth > 0){
            isEnemyInDamagedState = true;
            GetComponent<CircleCollider2D>().enabled = false;
            Color hitColor = new Color(1, 0, 0, 1);
            Color noHitColor = new Color(1, 1, 1, 0.5f);
        
            enemySpriteRenderer.color = noHitColor;
            yield return new WaitForSeconds(0.1f);

            for(float i = 0; i < 1f; i+= 0.1f){
                enemySpriteRenderer.enabled = false;
                yield return new WaitForSeconds(0.1f);
                enemySpriteRenderer.enabled = true;
                yield return new WaitForSeconds(0.1f);
            }

            enemySpriteRenderer.color = Color.white;
            GetComponent<CircleCollider2D>().enabled = true;
            isEnemyInDamagedState = false;
        }
    }

}
