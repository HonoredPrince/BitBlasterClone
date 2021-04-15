using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisionController : MonoBehaviour
{
    HUDController hudController;
    ShipAttack shipAttackController;
    GameController gameController;
    ScoreController scoreController;

    Coroutine currentFiringTypeRoutine, currentBerserkerRoutine;

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
            case "NukePowerUp":
                if(shipAttackController.GetAmountOfNukes() == 5){
                    scoreController.AddScore(100);
                }
                shipAttackController.AddNuke();
                hudController.UpdateNukesHUD(shipAttackController.GetAmountOfNukes());
                Destroy(collision.gameObject);
                break;
            case "TripleBulletPowerUp":
                if(shipAttackController.GetTypeOfFiringSystem() == "tripleBullet"){
                    StopCoroutine(this.currentFiringTypeRoutine); //Works but maybe not the optimal way
                    shipAttackController.shipHasSpecialBullet = false;
                }
                if(shipAttackController.GetTypeOfFiringSystem() == "laserStream"){
                    shipAttackController.DeactivateLaserMode();
                    StopCoroutine(this.currentFiringTypeRoutine); //Find a way of optmize this mess
                    shipAttackController.ResetFiringSystem();
                }
                this.currentFiringTypeRoutine = StartCoroutine(shipAttackController.ChangeTypeOfFiringSystemInSeconds("tripleBullet"));
                Destroy(collision.gameObject);
                break;
            case "ShipBerserkerPowerUp":
                if(shipAttackController.HasBerserkerMode() == true){
                    shipAttackController.DeactivateBerserkerMode();
                    StopCoroutine(this.currentBerserkerRoutine); //Works but maybe not the optimal way
                }
                this.currentBerserkerRoutine = StartCoroutine(shipAttackController.ActivateBerserkerMode());
                Destroy(collision.gameObject);
                break;
            case "LaserPowerUp":
                if(shipAttackController.GetTypeOfFiringSystem() == "laserStream"){
                    StopCoroutine(this.currentFiringTypeRoutine); //Works but maybe not the optimal way
                    shipAttackController.shipHasSpecialBullet = false;
                }
                if(shipAttackController.GetTypeOfFiringSystem() == "tripleBullet"){
                    StopCoroutine(this.currentFiringTypeRoutine); //Find a way of optmize this mess
                    shipAttackController.ResetFiringSystem();
                }
                this.currentFiringTypeRoutine = StartCoroutine(shipAttackController.ActivateLaserMode());
                Destroy(collision.gameObject);
                break;
        }
    }
}
