using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7Bomb : MonoBehaviour
{
    Rigidbody2D bulletRigidBody2D;
    bool canMove;
    Animator bombAnimator;
    float moveSpeed;
    [SerializeField] GameObject explosionCompoundCollider = null;
    Collider2D projectileCollider;
    SoundController soundController;

    bool hasExploded = false;
    
    void Awake(){
        projectileCollider = GetComponent<Collider2D>();
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();
        bulletRigidBody2D = GetComponentInChildren<Rigidbody2D>();
        this.canMove = true;
        moveSpeed = 15f;
        bombAnimator = GetComponent<Animator>();    
        Invoke("BombTimer", Random.Range(0.8f, 2f)); //Random.Range(0.8f, 1.7f), Random.Range(0.8f, 2f)
    }

    void Update(){
        if(canMove){
            //BulletMovement();
        }else{
            bulletRigidBody2D.gravityScale = 0f;
            this.bulletRigidBody2D.velocity = new Vector2(0f, 0f);
        }
    }

    void BulletMovement(){
        bulletRigidBody2D.velocity = transform.right * moveSpeed;
    }

    void BombTimer(){
        StartCoroutine(DelayedBombExplosion(1f));
    }

    public void DestroyBullet(){
        Destroy(transform.parent.gameObject);
    }
    
    IEnumerator DelayedBombExplosion(float delay){
        if(!this.hasExploded){
            this.canMove = false;
            yield return new WaitForSeconds(delay);
            this.hasExploded = true;
            projectileCollider.enabled = false;
            soundController.playSFX("purpleBombHit");
            bombAnimator.SetTrigger("bombExplosion");
            this.explosionCompoundCollider.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            this.explosionCompoundCollider.SetActive(false);
            yield return new WaitForSeconds(2.0f);
            DestroyBullet();
        }
    }
    
    IEnumerator InstantBombExplosion(){
        if(!this.hasExploded){
            this.canMove = false;
            this.hasExploded = true;
            projectileCollider.enabled = false;
            soundController.playSFX("purpleBombHit");
            bombAnimator.SetTrigger("bombExplosion");
            this.explosionCompoundCollider.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            this.explosionCompoundCollider.SetActive(false);
            yield return new WaitForSeconds(2.0f);
            DestroyBullet();
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Ship":
                StartCoroutine(InstantBombExplosion());
                break;
            case "BordersPoints":
                StartCoroutine(InstantBombExplosion());
                break;
            case "ShipBerserker":
                Destroy(this.gameObject);
                break;
        }
    }
}
