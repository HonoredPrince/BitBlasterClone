using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionMagnet : MonoBehaviour
{
    Rigidbody2D ammunitionRigidBody2D;
    [SerializeField]float movSpeed = 0f;
    Transform shipTransform;
    [SerializeField] Transform ammoParentTransform = null;

    void Awake(){
        this.shipTransform = GameObject.FindGameObjectWithTag("Ship").GetComponent<Transform>();
    }

    void AmmunitionMovement(){
        ammoParentTransform.position = Vector2.MoveTowards(transform.position, shipTransform.position, movSpeed * Time.deltaTime);
    }

    void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.tag == "Ship"){
            AmmunitionMovement();
        }
    } 
}
