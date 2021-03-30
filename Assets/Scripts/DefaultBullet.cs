using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    Rigidbody2D bulletRigidBody2D;
    float moveSpeed;

    void Awake(){
        bulletRigidBody2D = GetComponentInChildren<Rigidbody2D>();
        moveSpeed = 15f;
        //Only for initial tests, when garbage collector is not implemented
        //Invoke("DestroyBullet", 6f);
    }

    void Update(){
        BulletMovement();
    }

    void BulletMovement(){
        bulletRigidBody2D.velocity = transform.right * moveSpeed;
    }

    public void DestroyBullet(){
        Destroy(transform.parent.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Enemy1" || collision.gameObject.tag == "Enemy1_Splitted"){
            DestroyBullet();
        }
    }
}
