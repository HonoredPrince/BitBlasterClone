using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisionController : MonoBehaviour
{
    ShipAttack shipAttackController;

    void Awake(){
        shipAttackController = GetComponent<ShipAttack>();
    }

    void OnTriggerEnter2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Ammunition":
                shipAttackController.AddAmmo(1);
                Destroy(collision.gameObject);
                break;
        }
    }
}
