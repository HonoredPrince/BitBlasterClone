using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Movement : MonoBehaviour
{
    Transform shipTransform;
    float movSpeed;

    void Awake(){
        try{
            shipTransform = GameObject.FindGameObjectWithTag("Ship").GetComponent<Transform>();
        }catch(Exception e){
            Debug.Log("Ship not found: " + e);   
        }
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
