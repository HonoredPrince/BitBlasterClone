using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Movement : MonoBehaviour
{
    Transform shipTransform;
    float movSpeed;

    void Update(){
        shipTransform = GameObject.FindGameObjectWithTag("Ship").GetComponent<Transform>();
        movSpeed = 3f;
        Movement();
    }

    void Movement(){
        transform.position = Vector2.MoveTowards(transform.position, shipTransform.position, movSpeed*Time.deltaTime);
    }
}
