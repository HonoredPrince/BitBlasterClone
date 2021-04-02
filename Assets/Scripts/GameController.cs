using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject shipPlayer;
    bool isShipInDamagedState, isShipInvencible;
    int playerShield;
    int playerHealth;

    void Awake(){
        shipPlayer = GameObject.FindGameObjectWithTag("Ship");
        
        isShipInvencible = false; //God Mode
        
        isShipInDamagedState = false;
        playerShield = 1;
        playerHealth = 1;
    }

    IEnumerator playerDamage(){
        if(!isShipInDamagedState && isShipInvencible == false){
            isShipInDamagedState = true;
            if(playerShield > 0){
                string damageTarget = "Shield";
                StartCoroutine(DamageTaken(damageTarget));
                //Debug.Log("ShipShield: " + playerShield);
                //Debug.Log("ShipHealth: " + playerHealth);
            }else if(playerShield == 0 && playerHealth > 0){
                string damageTarget = "Health";
                StartCoroutine(DamageTaken(damageTarget));
                //Debug.Log("ShipShield: " + playerShield);
                //Debug.Log("ShipHealth: " + playerHealth); 
            }else{
                GameObject[] emitters = GameObject.FindGameObjectsWithTag("Emitter");
                foreach(GameObject emitter in emitters){
                    emitter.SetActive(false);
                }
                //TODO: Ship's death and handle the game's scenes resets, a.k.a Game Ending Handler
                shipPlayer.SetActive(false);
                yield return new WaitForSeconds(3f);
                SceneManager.LoadScene("MainGame");
            }
        }
    }

    IEnumerator DamageTaken(string damageTarget){
        if(damageTarget == "Shield"){
            playerShield--;
        }else{
            playerHealth--;
        }
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
}
