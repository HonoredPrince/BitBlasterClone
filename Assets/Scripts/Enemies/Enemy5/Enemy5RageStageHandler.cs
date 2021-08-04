using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5RageStageHandler : MonoBehaviour
{
    EnemyHealthManager enemyHealthManager;
    Animator enemy5Animator;
    SpriteRenderer enemySpriteRenderer;
    EnemyFollowerMovement enemyMovementHandler;

    void Awake(){
        enemyHealthManager = GetComponent<EnemyHealthManager>();
        enemy5Animator = GetComponent<Animator>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyMovementHandler = GetComponent<EnemyFollowerMovement>();
    }

    void Update(){
        if(enemyHealthManager.GetCurrentHealth() == 1){
            enemy5Animator.SetTrigger("Stage3Trigger");
            enemyMovementHandler.ChangeSpeed(1.5f);
        }else if(enemyHealthManager.GetCurrentHealth() == 2){
            enemy5Animator.SetTrigger("Stage2Trigger");
            enemyMovementHandler.ChangeSpeed(1.0f);
        }
    }
}
