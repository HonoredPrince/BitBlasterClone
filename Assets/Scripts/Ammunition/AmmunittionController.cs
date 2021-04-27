using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunittionController : MonoBehaviour
{
    [SerializeField]GameObject mininumMagnetCollider = null, maximumMagnetCollider = null;

    void Awake(){
        mininumMagnetCollider.SetActive(true);
        maximumMagnetCollider.SetActive(false);
        StartCoroutine(AmmoSpawnTime(10f));
    }

    IEnumerator AmmoSpawnTime(float ammoTimeActive){
        Color hitColor = new Color(1, 0, 0, 1);
        Color noHitColor = new Color(1, 1, 1, 0.5f);
        SpriteRenderer ammoSprite = GetComponent<SpriteRenderer>();
        
        yield return new WaitForSeconds(ammoTimeActive);
        
        ammoSprite.color = noHitColor;
        yield return new WaitForSeconds(0.1f);

        for(float i = 0; i < 2; i+= 0.3f){
            ammoSprite.enabled = false;
            yield return new WaitForSeconds(0.3f);
            ammoSprite.enabled = true;
            yield return new WaitForSeconds(0.3f);
        }
        ammoSprite.color = Color.white;
        
        Destroy(this.gameObject);
    }
}
