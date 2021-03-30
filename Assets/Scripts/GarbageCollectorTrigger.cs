using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollectorTrigger : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision){
        Destroy(collision.gameObject.transform.parent.gameObject);
    }

}
