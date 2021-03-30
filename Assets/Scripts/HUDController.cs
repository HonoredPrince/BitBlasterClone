using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] Image boostBarImage = null;

    public void DecreaseBoostBar(float amount){
        boostBarImage.fillAmount -= amount;
    }

    public void IncreaseBoostBar(float amount){
        boostBarImage.fillAmount += amount;   
    }
}
