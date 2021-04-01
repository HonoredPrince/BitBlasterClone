using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunittionController : MonoBehaviour
{
    [SerializeField]GameObject mininumMagnetCollider = null, maximumMagnetCollider = null;

    void Awake(){
        mininumMagnetCollider.SetActive(true);
        maximumMagnetCollider.SetActive(false);
    }
}
