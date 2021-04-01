using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] Image boostBarImage = null;
    ShipAttack shipAttackHandler;
    [SerializeField]Text shipAmmoText = null;

    void Awake(){
        shipAttackHandler = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipAttack>();
    }

    void FixedUpdate(){
        shipAmmoText.text = this.shipAttackHandler.GetShipCurrentAmmo().ToString();
    }

    public void DecreaseBoostBar(float amount){
        boostBarImage.fillAmount -= amount;
    }

    public void IncreaseBoostBar(float amount){
        boostBarImage.fillAmount += amount;   
    }
}
