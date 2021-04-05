using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisionController : MonoBehaviour
{
    HUDController hudController;
    ShipAttack shipAttackController;
    GameController gameController;
    ScoreController scoreController;

    void Awake(){
        shipAttackController = GetComponent<ShipAttack>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        scoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
        hudController = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDController>();
    }

    void OnTriggerEnter2D(Collider2D collision){
        EnemyCollisionDetection(collision);
        PowerUpsCollisionDetection(collision);
    }

    void OnTriggerStay2D(Collider2D collision){
        EnemyCollisionDetection(collision);
    }

    void EnemyCollisionDetection(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Enemy1":
                gameController.DestroyEnemyIfValid(collision.gameObject);
                gameController.StartCoroutine("playerDamage");
                break;
            case "Enemy1_Splitted":
                gameController.DestroyEnemyIfValid(collision.gameObject);
                gameController.StartCoroutine("playerDamage");
                break;
            case "Enemy2":
                gameController.DestroyEnemyIfValid(collision.gameObject);
                gameController.StartCoroutine("playerDamage");
                break;
            case "Enemy3":
                gameController.DestroyEnemyIfValid(collision.gameObject);
                gameController.StartCoroutine("playerDamage");
                break;
        }
    }

    void PowerUpsCollisionDetection(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Ammunition":
                shipAttackController.AddAmmo(1);
                Destroy(collision.gameObject);
                break;
            case "ShieldPowerUp":
                if(gameController.GetShipShield() == 5){
                    scoreController.AddScore(50);
                }
                gameController.AddShield(1);
                hudController.UpdateShieldHUD(gameController.GetShipShield());
                Destroy(collision.gameObject);
                break;
            case "TripleBulletPowerUp":
                StartCoroutine(shipAttackController.ChangeTypeOfFiringSystemInSeconds("tripleBullet", 30f));
                Destroy(collision.gameObject);
                break;
        }
    }
}
