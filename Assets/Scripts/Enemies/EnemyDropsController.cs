using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropsController : MonoBehaviour
{
    [SerializeField]GameObject[] enemyDrops = null;

    public void DropAmmo(Transform enemyTransform){
        GameObject ammoDropped = Instantiate(enemyDrops[0], enemyTransform.position, Quaternion.identity);
    }

    public void DropShieldPowerUp(Transform enemyTransform){
        GameObject shieldDropped = Instantiate(enemyDrops[1], enemyTransform.position, Quaternion.identity);
    }  
    
    public void DropTripleBulletPowerUp(Transform enemyTransform){
        GameObject shieldDropped = Instantiate(enemyDrops[2], enemyTransform.position, Quaternion.identity);
    } 

    public void DropBerserkerPowerUp(Transform enemyTransform){
        GameObject shieldDropped = Instantiate(enemyDrops[3], enemyTransform.position, Quaternion.identity);
    } 
}
