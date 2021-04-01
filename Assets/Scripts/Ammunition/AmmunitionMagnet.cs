using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionMagnet : MonoBehaviour
{
    Rigidbody2D ammunitionRigidBody2D;
    [SerializeField]float movSpeed = 0f;
    Transform shipTransform, ammoTransform;

    void Awake(){
        this.shipTransform = GameObject.FindGameObjectWithTag("Ship").GetComponent<Transform>();
        this.ammoTransform = this.transform.parent;
    }

    void AmmunitionMovement(){
        ammoTransform.position = Vector2.MoveTowards(transform.position, shipTransform.position, movSpeed * Time.deltaTime);
    }

    void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.tag == "Ship"){
            AmmunitionMovement();
        }
    } 
}
