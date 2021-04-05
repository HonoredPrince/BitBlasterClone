using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAttack : MonoBehaviour
{
    [SerializeField] GameObject defaultBullet = null;
    [SerializeField]Transform[] shootingPoints = null;

    int shipAmmo;
    float fireRate = 0.1f;
    bool fireAllowed;

    //TODO: Figure it out how is gonna work the data information of the type of bullet fired
    string typeOfFiringSystem;

    void Awake(){
        fireAllowed = true;
        shipAmmo = 100;
        typeOfFiringSystem = "defaultBullet";
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            FireBullet();
        }    
    }

    public int GetShipCurrentAmmo(){
        return this.shipAmmo;
    }

    public void AddAmmo(int amount){
        this.shipAmmo += amount;
    }

    void FireBullet(){
        switch(this.typeOfFiringSystem){
            case "defaultBullet":
                StartCoroutine(FireDefaultBullet());
                break;
            case "tripleBullet":
                StartCoroutine(FireTripleBullet());
                break;
        }
    }

    //TODO: The Ship's attack firing system changing to the other 2 types of bullets: triple-fire e laser
    public IEnumerator ChangeTypeOfFiringSystemInSeconds(string typeOfFireSystem, float timeActiveInSeconds){
        //TODO: Fix the issue with getting the power up while in the time frame of the same type of powerup,
        //will have to just reset time, and not letting one routine call interfere with another
        this.typeOfFiringSystem = typeOfFireSystem;
        yield return new WaitForSeconds(timeActiveInSeconds);
        this.typeOfFiringSystem = "defaultBullet";
    }

    IEnumerator FireDefaultBullet(){
        if(fireAllowed && this.shipAmmo > 0){
            fireAllowed = false;
            Instantiate(defaultBullet, shootingPoints[0].position, transform.rotation);
            this.shipAmmo--;
            yield return new WaitForSeconds(fireRate);
            fireAllowed = true;
        }
    }
    IEnumerator FireTripleBullet(){
        if(fireAllowed && this.shipAmmo > 0){
            fireAllowed = false;
            Instantiate(defaultBullet, shootingPoints[0].position, shootingPoints[0].rotation);
            Instantiate(defaultBullet, shootingPoints[1].position, shootingPoints[1].rotation);
            Instantiate(defaultBullet, shootingPoints[2].position, shootingPoints[2].rotation);
            this.shipAmmo--;
            yield return new WaitForSeconds(fireRate);
            fireAllowed = true;
        }
    }

}
