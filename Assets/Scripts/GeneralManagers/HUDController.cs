using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] Image boostBarImage = null;
    [SerializeField]Text shipAmmoText = null;
    [SerializeField]Text shipScoreText = null;

    ShipAttack shipAttackHandler;
    ScoreController shipScoreController;

    void Awake(){
        shipAttackHandler = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipAttack>();
        shipScoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
    }

    void FixedUpdate(){
        shipAmmoText.text = this.shipAttackHandler.GetShipCurrentAmmo().ToString();
        shipScoreText.text = "Score: " + this.shipScoreController.GetPlayerScore().ToString();
    }

    public void DecreaseBoostBar(float amount){
        boostBarImage.fillAmount -= amount;
    }

    public void IncreaseBoostBar(float amount){
        boostBarImage.fillAmount += amount;   
    }
}
