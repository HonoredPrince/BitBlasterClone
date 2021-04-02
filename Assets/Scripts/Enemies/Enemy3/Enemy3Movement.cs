using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Movement : MonoBehaviour
{
    Transform shipTransform;
    float movSpeed;

    void Awake(){
        //TODO: Have to fix this line sometimes conflicting with the ship destruction/deactivation
        //probably because the emitters still emits enemy type 3 even after the game's ending delay time
        shipTransform = GameObject.FindGameObjectWithTag("Ship").GetComponent<Transform>();
    }

    void Update(){
        movSpeed = 3f;
        Movement();
    }

    void Movement(){
        if(shipTransform != null){
            transform.position = Vector2.MoveTowards(transform.position, shipTransform.position, movSpeed*Time.deltaTime);
        }
    }
}
