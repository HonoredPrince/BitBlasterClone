using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmitter : MonoBehaviour
{
    [Header("Enemy Selection")] 
    [SerializeField]GameObject[] enemyPrefab = null;
    
    [Header("Emitter Limits")] 
    [SerializeField]Transform emissionStartPoint = null;
    [SerializeField]Transform emissionEndPoint = null;
    
    [Header("Emitter Orientation")] 
    [SerializeField]string emissionOrientationType = null;
    [SerializeField]string emitterType = null;
    [SerializeField]int directionOfEmission = 0;

    void Awake(){
        InvokeRepeating("TestSpawn", 1f, 3f);
    }

    //For DEBUG only
    void TestSpawn(){
        float chance = Random.Range(0, 100);
        if(chance <= 20){
            SpawnEnemy(this.enemyPrefab[1], this.emissionOrientationType, this.directionOfEmission);
        }else{  
            SpawnEnemy(this.enemyPrefab[0], this.emissionOrientationType, this.directionOfEmission);
        }
    }

    Vector2 RandomEmissionPointInEmitterLimits(string emitterType){
        Vector2 worldEmissionStartPoint = emissionStartPoint.transform.TransformPoint(emissionStartPoint.position);
        Vector2 worldEmissionEndPoint = emissionEndPoint.transform.TransformPoint(emissionEndPoint.position);
        float randomXPos = 0, randomYPos = 0;
        
        if(emitterType == "Side"){
            randomXPos = Random.Range(worldEmissionStartPoint.x/2, worldEmissionEndPoint.x/2);
            randomYPos = Random.Range(emissionStartPoint.position.y, emissionEndPoint.position.y);
        }else if(emitterType == "Top"){
            randomXPos = Random.Range(emissionStartPoint.position.x, emissionEndPoint.position.x);
            randomYPos = Random.Range(worldEmissionStartPoint.y/2, worldEmissionEndPoint.y/2);
        }
        
        Vector2 randomEmissionPoint = new Vector2(randomXPos, randomYPos);
        //Debug.Log(randomEmissionPoint);
        return randomEmissionPoint; 
    }

    void SpawnEnemy(GameObject enemyPrefab, string orientation, int direction){
        GameObject enemySpawned;
        switch(enemyPrefab.gameObject.tag){
            case "Enemy1":
                enemySpawned = Instantiate(enemyPrefab,RandomEmissionPointInEmitterLimits(this.emitterType),transform.rotation);
                Enemy1Movement enemySpawnedMovementController = enemySpawned.GetComponent<Enemy1Movement>();
                enemySpawnedMovementController.direction = direction;
                enemySpawnedMovementController.typeOfDirection = orientation;
                break;
            case "Enemy3":
                enemySpawned = Instantiate(enemyPrefab,RandomEmissionPointInEmitterLimits(this.emitterType),Quaternion.identity);
                break;
        }
        
    }
}
