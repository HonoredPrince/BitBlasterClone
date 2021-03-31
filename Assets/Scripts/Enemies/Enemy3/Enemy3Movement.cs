using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Movement : MonoBehaviour
{
    Transform shipPosition;
    Rigidbody2D enemy1RigidBody2D;
    float movSpeed;

    void Update(){
        shipPosition = GameObject.FindGameObjectWithTag("Ship").GetComponent<Transform>();
        movSpeed = 3f;
        Movement();
    }

    void Movement(){
        transform.position = Vector2.MoveTowards(transform.position, shipPosition.position, movSpeed*Time.deltaTime);
    }
}
