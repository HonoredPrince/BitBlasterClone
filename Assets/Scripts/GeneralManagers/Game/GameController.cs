using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] HUDController hudController = null;
    LevelLoader levelLoaderController;
    SoundController soundController;
    GameObject shipPlayer;
    [SerializeField] GameObject[] shipDeathObjects = null; 
    bool isShipInDamagedState, isShipInvencible;
    int playerShield;

    [Header("Borders Tranforms")]
    [SerializeField] Transform leftBorder = null;
    [SerializeField] Transform rightBorder = null; 
    [SerializeField] Transform topBorder = null;
    [SerializeField] Transform bottomBorder = null;
    
    [Header("GarbageCollectors Tranforms")]
    [SerializeField] Transform leftGarbageCollector = null;
    [SerializeField] Transform rightGarbageCollector = null;
    [SerializeField] Transform topGarbageCollector = null;
    [SerializeField] Transform bottomGarbageCollector = null;                      
                                
    [Header("Emitters Tranforms")]
    [SerializeField] Transform leftEmitter = null;
    [SerializeField] Transform rightEmitter = null;
    [SerializeField] Transform topEmitter = null;
    [SerializeField] Transform bottomEmitter = null;

    void Awake(){
        shipPlayer = GameObject.FindGameObjectWithTag("Ship");
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
        levelLoaderController = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();

        isShipInvencible = false; //God Mode for Debug purposes
        
        isShipInDamagedState = false;
        playerShield = 3; 
        hudController.UpdateShieldHUD(playerShield);
        soundController.playMusic();

        SetEmittersPositions();
        SetGarbageCollectorsPositions();
    }
    
    void FixedUpdate(){
        //Just for debug purposes in the Unity Editor Runtime
        SetEmittersPositions();
        SetGarbageCollectorsPositions();
    }

    //TODO: Move Ship Health Handling to another script later
    IEnumerator playerDamage(){
        if(!isShipInDamagedState && isShipInvencible == false){
            isShipInDamagedState = true;
            if(playerShield > 0){
                StartCoroutine(DamageTaken());
                hudController.UpdateShieldHUD(this.playerShield);
                //Debug.Log("ShipShield: " + playerShield);
            }else{
                //TODO: Ship's death and handle the game's scenes resets, a.k.a Game Ending Handler
                GameObject[] emitters = GameObject.FindGameObjectsWithTag("Emitter");
                foreach(GameObject emitter in emitters){
                    emitter.SetActive(false);
                }
                shipPlayer.SetActive(false);
                GameObject shipDeathAnimObj = Instantiate(shipDeathObjects[0], shipPlayer.transform.position, shipPlayer.transform.rotation);
                soundController.playSFX("shipDeath");
                //For now, it's better to instantaneously end the game upon death hit, until find a way
                //for not get NullReference on emmiters spawn objects on "game end" delay
                yield return new WaitForSeconds(0.8f);
                Destroy(shipDeathAnimObj);
                yield return new WaitForSeconds(1f);
                levelLoaderController.LoadLevelWithName("GameOverScene");
            }
        }
    }

    IEnumerator DamageTaken(){
        playerShield--;
        soundController.playSFX("shipHitDamage");
        Color hitColor = new Color(1, 0, 0, 1);
        Color noHitColor = new Color(1, 1, 1, 0.5f);
        SpriteRenderer playerSprite = shipPlayer.GetComponent<SpriteRenderer>();
        SpriteRenderer thrustsSprites = GameObject.FindGameObjectWithTag("ShipThrusts").GetComponent<SpriteRenderer>();
        
        playerSprite.color = noHitColor;
        thrustsSprites.color = noHitColor;
        yield return new WaitForSeconds(0.1f);

        for(float i = 0; i < 1; i+= 0.1f){
            playerSprite.enabled = false;
            thrustsSprites.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerSprite.enabled = true;
            thrustsSprites.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        playerSprite.color = Color.white;
        thrustsSprites.color = Color.white;
        isShipInDamagedState = false;
    }

    public void DestroyEnemyIfValid(GameObject enemyPrefab){
        if(!isShipInDamagedState){
            Destroy(enemyPrefab);
        }
    }

    public void AddShield(int amount){
        if(this.playerShield < 5){
            this.playerShield += amount;
        }
    }

    public void SetPlayerInvencible(bool status){
        this.isShipInvencible = status;
    }

    public int GetShipShield(){
        return this.playerShield;
    }

    void SetEmittersPositions(){
        /*Debug.Log("LeftBorder:" + leftBorder.position);
        Debug.Log("RightBorder:" + rightBorder.position);
        Debug.Log("TopBorder:" + topBorder.position);
        Debug.Log("BottomBorder:" + bottomBorder.position);*/
        
        leftEmitter.position = new Vector3(leftBorder.position.x - 6f, leftEmitter.position.y, leftEmitter.position.z);
        rightEmitter.position = new Vector3(rightBorder.position.x + 6f, rightEmitter.position.y, rightEmitter.position.z);
        topEmitter.position = new Vector3(bottomEmitter.position.x, topBorder.position.y + 2f, topEmitter.position.z);
        bottomEmitter.position = new Vector3(bottomEmitter.position.x, bottomBorder.position.y - 2f, bottomEmitter.position.z);
    
        /*Debug.Log("LeftEmitter: " + leftEmitter.position);
        Debug.Log("RightEmitter: " + rightEmitter.position);
        Debug.Log("TopEmitter: " + topEmitter.position);
        Debug.Log("BottomEmitter: " + bottomEmitter.position);*/
    }
    
    void SetGarbageCollectorsPositions(){
        /*Debug.Log("LeftBorder:" + leftBorder.position);
        Debug.Log("RightBorder:" + rightBorder.position);
        Debug.Log("TopBorder:" + topBorder.position);
        Debug.Log("BottomBorder:" + bottomBorder.position);*/
        
        leftGarbageCollector.position = new Vector3(leftBorder.position.x - 11.77f, leftGarbageCollector.position.y, leftGarbageCollector.position.z);
        rightGarbageCollector.position = new Vector3(rightBorder.position.x + 11.77f, rightGarbageCollector.position.y, rightGarbageCollector.position.z);
        topGarbageCollector.position = new Vector3(bottomGarbageCollector.position.x, topBorder.position.y + 5f, topGarbageCollector.position.z);
        bottomGarbageCollector.position = new Vector3(bottomGarbageCollector.position.x, bottomBorder.position.y - 5f, bottomGarbageCollector.position.z);
    
        /*Debug.Log("LeftGarbageCollector: " + leftGarbageCollector.position);
        Debug.Log("RightGarbageCollector: " + rightGarbageCollector.position);
        Debug.Log("TopGarbageCollector: " + topGarbageCollector.position);
        Debug.Log("BottomGarbageCollector: " + bottomGarbageCollector.position);*/
    }
}
