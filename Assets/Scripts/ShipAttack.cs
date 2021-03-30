using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAttack : MonoBehaviour
{
    [SerializeField] GameObject defaultBullet = null;
    [SerializeField]Transform shootingPoint = null;

    float fireRate = 0.1f;
    bool fireAllowed;

    void Awake(){
        fireAllowed = true;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            StartCoroutine(FireBullet());
        }    
    }

    IEnumerator FireBullet(){
        if(fireAllowed){
            fireAllowed = false;
            Instantiate(defaultBullet, shootingPoint.position, transform.rotation);
            yield return new WaitForSeconds(fireRate);
            fireAllowed = true;
        }
    }
}
