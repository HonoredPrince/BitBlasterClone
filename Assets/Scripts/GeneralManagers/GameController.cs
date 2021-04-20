using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] HUDController hudController = null;
    SoundController soundController;
    GameObject shipPlayer;
    [SerializeField] GameObject[] shipDeathObjects = null; 
    bool isShipInDamagedState, isShipInvencible;
    int playerShield;

    void Awake(){
        shipPlayer = GameObject.FindGameObjectWithTag("Ship");
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();

        isShipInvencible = false; //God Mode for Debug purposes
        
        isShipInDamagedState = false;
        playerShield = 3; 
        hudController.UpdateShieldHUD(playerShield);
        soundController.playMusic();
    }

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
                SceneManager.LoadScene("StartMenu");
            }
        }
    }

    IEnumerator DamageTaken(){
        playerShield--;
        soundController.playSFX("shipHitDamage");
        Color hitColor = new Color(1, 0, 0, 1);
        Color noHitColor = new Color(1, 1, 1, 0.5f);
        SpriteRenderer playerSprite = shipPlayer.GetComponent<SpriteRenderer>();
        playerSprite.color = noHitColor;
        yield return new WaitForSeconds(0.1f);

        for(float i = 0; i < 1; i+= 0.1f){
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        playerSprite.color = Color.white;
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
}
