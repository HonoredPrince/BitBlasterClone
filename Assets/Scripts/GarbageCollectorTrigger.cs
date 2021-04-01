using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollectorTrigger : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision){
        switch(collision.gameObject.tag){
            case "Bullet1":
                Destroy(collision.gameObject.transform.parent.gameObject);
                break;
            case "Enemy1":
                Destroy(collision.gameObject);
                break;
            case "Enemy1_Splitted":
                Destroy(collision.gameObject);
                break;
        }
    }
}
