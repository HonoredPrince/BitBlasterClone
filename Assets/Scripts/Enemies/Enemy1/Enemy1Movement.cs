using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Movement : MonoBehaviour
{
    Rigidbody2D enemy1RigidBody2D;
    float movSpeed;
    
    public string typeOfDirection;
    public int direction;

    void Update(){
        movSpeed = 1f;
        Movement();
    }

    void Movement(){
        if(typeOfDirection == "Vertical"){
            if(direction > 0){
                transform.Translate(transform.up * movSpeed * Time.deltaTime, Space.Self);
            }else{
                transform.Translate(-transform.up * movSpeed * Time.deltaTime, Space.Self);
            }
        }else if(typeOfDirection == "Horizontal"){
            if(direction > 0){
                transform.Translate(-transform.right * movSpeed * Time.deltaTime, Space.Self);
            }else{
                transform.Translate(transform.right * movSpeed * Time.deltaTime, Space.Self);
            }
        }
    }
}
