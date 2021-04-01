using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropsController : MonoBehaviour
{
    [SerializeField]GameObject ammoPrefab = null;

    public void DropAmmo(Transform enemyTransform){
        GameObject ammoDropped = Instantiate(ammoPrefab, enemyTransform.position, Quaternion.identity);
    }
}
