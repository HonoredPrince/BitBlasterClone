using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAttack : MonoBehaviour
{
    [SerializeField] GameObject defaultBullet = null;
    [SerializeField] Transform[] shootingPoints = null;
    [SerializeField] GameObject shipBerserker = null;

    HUDController hudController;
    GameController gameController;

    int shipAmmo;
    float fireRate = 0.1f;
    bool fireAllowed;
    bool hasBerserkerMode;

    string typeOfFiringSystem;
    [HideInInspector] public bool shipHasSpecialBullet;

    void Awake(){
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        hudController = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDController>();
        fireAllowed = true;
        hasBerserkerMode = false;
        shipAmmo = 100;
        typeOfFiringSystem = "defaultBullet";
        shipHasSpecialBullet = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            FireBullet();
        }   

        if(shipHasSpecialBullet){
            hudController.DecreaseWeaponTypeBar(Time.deltaTime/10f);
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
        //See the current solution on ShipCollisionPowerUp switch(case)...
        //Debug.Log(Time.time);
        hudController.SetWeaponTypeBarActive();
        shipHasSpecialBullet = true;
        this.typeOfFiringSystem = typeOfFireSystem;
        hudController.SetBulletTypeSprite(this.typeOfFiringSystem);
        yield return new WaitForSeconds(timeActiveInSeconds);
        //Debug.Log(Time.time);
        this.typeOfFiringSystem = "defaultBullet";
        shipHasSpecialBullet = false;
        hudController.DeactivateWeaponTypeBar();
        hudController.SetBulletTypeSprite(this.typeOfFiringSystem);
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

    public IEnumerator ActivateBerserkerMode(float timeActiveInSeconds){
        //Debug.Log(Time.time);
        this.hasBerserkerMode = true;
        this.gameController.SetPlayerInvencible(true);
        this.shipBerserker.SetActive(true);

        yield return new WaitForSeconds(timeActiveInSeconds);
        //Debug.Log(Time.time);
        
        this.hasBerserkerMode = false;
        this.gameController.SetPlayerInvencible(false);
        this.shipBerserker.SetActive(false);
    }

    public void DeactivateBerserkerMode(){
        this.gameController.SetPlayerInvencible(false);
        this.shipBerserker.SetActive(false);
    }

    public bool HasBerserkerMode(){
        return this.hasBerserkerMode;
    }

    public string GetTypeOfFiringSystem(){
        return this.typeOfFiringSystem;
    }

}
