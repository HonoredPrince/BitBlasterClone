using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAttack : MonoBehaviour
{
    [SerializeField] GameObject defaultBullet = null;
    [SerializeField]Transform shootingPoint = null;

    int shipAmmo;
    float fireRate = 0.1f;
    bool fireAllowed;

    void Awake(){
        fireAllowed = true;
        shipAmmo = 100;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            StartCoroutine(FireBullet());
        }    
    }

    public int GetShipCurrentAmmo(){
        return this.shipAmmo;
    }

    public void AddAmmo(int amount){
        this.shipAmmo += amount;
    }

    IEnumerator FireBullet(){
        if(fireAllowed && this.shipAmmo > 0){
            fireAllowed = false;
            Instantiate(defaultBullet, shootingPoint.position, transform.rotation);
            this.shipAmmo--;
            yield return new WaitForSeconds(fireRate);
            fireAllowed = true;
        }
    }
}
