using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisionController : MonoBehaviour
{
    ShipAttack shipAttackController;
    GameController gameController;

    void Awake(){
        shipAttackController = GetComponent<ShipAttack>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Ammunition":
                shipAttackController.AddAmmo(1);
                Destroy(collision.gameObject);
                break;
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

    void OnTriggerStay2D(Collider2D collision){
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
}
