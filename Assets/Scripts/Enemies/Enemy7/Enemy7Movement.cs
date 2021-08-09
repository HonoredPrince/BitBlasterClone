using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7Movement : MonoBehaviour
{
    Transform enemyTransform;
    SpriteRenderer enemySprite;
    Transform[] positionPoints = new Transform[2];
    [SerializeField] float enemyMovSpeed = 0;
    bool isRight;
    int idTarget;
    [HideInInspector] public bool canMove;

    void Awake()
    {
        positionPoints[0] = GameObject.FindGameObjectWithTag("Enemy7LeftPositionPoint").GetComponent<Transform>();
        positionPoints[1] = GameObject.FindGameObjectWithTag("Enemy7RightPositionPoint").GetComponent<Transform>();
        
        enemyTransform = GetComponent<Transform>();
        enemySprite = GetComponent<SpriteRenderer>();
        //enemyTransform.position = positionPoints[0].position;   
        //idTarget = 1;  
        SetIdTargetBasedOnTheMostFarAwayPosition();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyTransform != null && canMove){
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, positionPoints[idTarget].position, enemyMovSpeed * Time.deltaTime);
            if(enemyTransform.position == positionPoints[idTarget].position){
                idTarget += 1;
                if(idTarget == positionPoints.Length){
                    idTarget = 0;
                }
            }
            if(positionPoints[idTarget].position.x < enemyTransform.position.x && isRight == true){
                FlipHandler();
            }else if(positionPoints[idTarget].position.x > enemyTransform.position.x && isRight == false){
                FlipHandler();
            }
        }
    }

    void FlipHandler(){
        isRight = !isRight;
        enemySprite.flipX = !enemySprite.flipX;
    }

    void SetIdTargetBasedOnTheMostFarAwayPosition(){
        if(Vector3.Distance(transform.position, positionPoints[0].position) > Vector3.Distance(transform.position, positionPoints[1].position)){
            idTarget = 0;
        }else{
            idTarget = 1;
        }
        //Debug.Log(idTarget);
    }

}
