using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine.UI;
using UnityEngine;

public class ShipAttack : MonoBehaviour
{
    [SerializeField] GameObject nukeWhiteScreen = null;

    [SerializeField] GameObject defaultBullet = null;
    [SerializeField] Transform[] shootingPoints = null;
    [SerializeField] GameObject shipBerserker = null;
    [SerializeField] GameObject shipLaser = null;


    HUDController hudController;
    GameController gameController;
    ScoreController scoreController;
    SoundController soundController;

    [HideInInspector] public bool shipHasSpecialBullet;
    int shipAmmo;
    float fireRate = 0.1f;
    bool fireAllowed;
    
    bool hasBerserkerMode;
    
    bool hasLaserMode;

    int amountOfNukes;
    float nukeDelayTime, weaponsDelayTime;
    bool canDeployNuke;
    
    string typeOfFiringSystem;
    

    void Awake(){
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        scoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
        hudController = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDController>();
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();

        shipAmmo = 100;
        amountOfNukes = 3;
        
        nukeDelayTime = 8f;
        weaponsDelayTime = 10f;

        fireAllowed = true;
        hasBerserkerMode = false;
        hasLaserMode = false;
        shipHasSpecialBullet = false;
        canDeployNuke = true;

        typeOfFiringSystem = "defaultBullet";

        hudController.UpdateNukesHUD(GetAmountOfNukes());
        hudController.SetNukeCooldownTimersActive(false);
    }

    void Update()
    {
        //Debug.Log(this.typeOfFiringSystem);
        //Debug.Log(this.hasLaserMode);
        
        if(Input.GetMouseButtonDown(0)){
            FireBullet();
        }   

        if(Input.GetMouseButtonDown(1) && amountOfNukes > 0 && canDeployNuke){
            StartCoroutine(DeployNuke());
        }

        if(shipHasSpecialBullet){
            hudController.DecreaseWeaponTypeBar(Time.deltaTime/this.weaponsDelayTime);
        } 

        if(!canDeployNuke){
            hudController.DecreaseNukeCooldownTimers(Time.deltaTime/this.nukeDelayTime);
        }
    }

    public int GetShipCurrentAmmo(){
        return this.shipAmmo;
    }

    public int GetAmountOfNukes(){
        return this.amountOfNukes;
    }
    
    public void AddAmmo(int amount){
        this.shipAmmo += amount;
    }

    public void AddNuke(){
        if(amountOfNukes < 5){
            amountOfNukes++;
        }
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

    IEnumerator FireDefaultBullet(){
        if(fireAllowed && this.shipAmmo > 0){
            fireAllowed = false;
            soundController.playSFX("shipFiring");
            Instantiate(defaultBullet, shootingPoints[0].position, transform.rotation);
            this.shipAmmo--;
            yield return new WaitForSeconds(fireRate);
            fireAllowed = true;
        }
    }

    IEnumerator FireTripleBullet(){
        if(fireAllowed && this.shipAmmo > 0){
            fireAllowed = false;
            soundController.playSFX("shipTripleFiring");
            Instantiate(defaultBullet, shootingPoints[0].position, shootingPoints[0].rotation);
            Instantiate(defaultBullet, shootingPoints[1].position, shootingPoints[1].rotation);
            Instantiate(defaultBullet, shootingPoints[2].position, shootingPoints[2].rotation);
            this.shipAmmo--;
            yield return new WaitForSeconds(fireRate);
            fireAllowed = true;
        }
    }

    public void ResetFiringSystem(){
        fireAllowed = true;
        this.typeOfFiringSystem = "defaultBullet";
        shipHasSpecialBullet = false;
        hudController.DeactivateWeaponTypeBar();
        hudController.SetWeaponWeaponTypeBarColor(new Color32(119, 255, 254, 255));
        hudController.SetBulletTypeSprite(this.typeOfFiringSystem);
    }

    //TODO: The Ship's attack firing system changing to the other 2 types of bullets: triple-fire e laser
    public IEnumerator ChangeTypeOfFiringSystemInSeconds(string typeOfFireSystem){
        //See the current solution on ShipCollisionPowerUp switch(case)...
        //Debug.Log(Time.time);
        hudController.SetWeaponTypeBarActive();
        shipHasSpecialBullet = true;
        this.typeOfFiringSystem = typeOfFireSystem;
        hudController.SetBulletTypeSprite(this.typeOfFiringSystem);
        yield return new WaitForSeconds(this.weaponsDelayTime);
        //Debug.Log(Time.time);
        ResetFiringSystem();
    }

    public IEnumerator ActivateLaserMode(){
        //Debug.Log(Time.time);
        this.fireAllowed = false;
        this.hasLaserMode = true;
        this.shipLaser.SetActive(true);
        hudController.SetWeaponTypeBarActive();
        hudController.SetWeaponWeaponTypeBarColor(new Color32(253, 47, 25, 255));
        shipHasSpecialBullet = true;
        this.typeOfFiringSystem = "laserStream";
        hudController.SetBulletTypeSprite("laserStream");
        
        yield return new WaitForSeconds(this.weaponsDelayTime);
        //Debug.Log(Time.time);
    
        this.shipLaser.SetActive(false);
        hasLaserMode = false;
        ResetFiringSystem();
    }

    public void DeactivateLaserMode(){
        this.shipLaser.SetActive(false);
    }

    public IEnumerator ActivateBerserkerMode(){
        //Debug.Log(Time.time);
        this.hasBerserkerMode = true;
        this.gameController.SetPlayerInvencible(true);
        this.shipBerserker.SetActive(true);

        yield return new WaitForSeconds(this.weaponsDelayTime);
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

    public bool HasLaserMode(){
        return this.hasLaserMode;
    }

    public string GetTypeOfFiringSystem(){
        return this.typeOfFiringSystem;
    }

    IEnumerator DeployNuke(){
        StartCoroutine(ActivateNukeDelay(this.nukeDelayTime));
        
        soundController.playSFX("nukeDeploy");
        yield return new WaitForSeconds(1f);
        
        //CameraShaker.Instance.ShakeOnce(16f, 32f, 0.1f, 1f); are the best values for now
        CameraShaker.Instance.ShakeOnce(16f, 32f, 0.1f, 1f);
        StartCoroutine(ActivateNukeWhiteScreen());
        this.amountOfNukes--;
        hudController.UpdateNukesHUD(GetAmountOfNukes());
        
        GameObject[] allEnemiesType1 = GameObject.FindGameObjectsWithTag("Enemy1");
        GameObject[] allEnemiesType1Splitted = GameObject.FindGameObjectsWithTag("Enemy1_Splitted");
        GameObject[] allEnemiesType2 = GameObject.FindGameObjectsWithTag("Enemy2");
        GameObject[] allEnemiesType3 = GameObject.FindGameObjectsWithTag("Enemy3");

        NukeDamageOnEnemies(allEnemiesType1, "Enemy1");
        NukeDamageOnEnemies(allEnemiesType1Splitted, "Enemy1_Splitted");
        NukeDamageOnEnemies(allEnemiesType2, "Enemy2");
        NukeDamageOnEnemies(allEnemiesType3, "Enemy3");
    }

    void NukeDamageOnEnemies(GameObject[] allEnemiesFromOneType, string typeToAddScore){
        foreach(GameObject enemie in allEnemiesFromOneType){
            Renderer enemieRenderer = enemie.GetComponent<Renderer>(); //Have to see if this works as a way to destroy only enemies in câmera view
            EnemyCollisionHandler enemieDropsController = enemie.GetComponent<EnemyCollisionHandler>();
            if(enemieRenderer.isVisible){
                Destroy(enemie);
                switch(typeToAddScore){
                    case "Enemy1":
                        scoreController.AddScore(10);
                        enemieDropsController.DropItem();
                        break;
                    case "Enemy1_Splitter":
                        scoreController.AddScore(20);
                        enemieDropsController.DropItem();
                        break;
                    case "Enemy2":
                        scoreController.AddScore(20);
                        enemieDropsController.DropItem();
                        break;
                    case "Enemy3":
                        scoreController.AddScore(30);
                        enemieDropsController.DropItem();
                        break;
                }
            }
        }
    }

    IEnumerator ActivateNukeDelay(float delayTime){
        canDeployNuke = false;
        hudController.SetNukeCooldownTimersActive(true);
        hudController.FillNukeCooldownTimers();
        yield return new WaitForSeconds(delayTime);
        hudController.SetNukeCooldownTimersActive(false);
        canDeployNuke = true;
    }

    IEnumerator ActivateNukeWhiteScreen(){
        Image nukeWhiteScreenImg = nukeWhiteScreen.GetComponent<Image>();
        nukeWhiteScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        nukeWhiteScreen.SetActive(false);
    }
}
