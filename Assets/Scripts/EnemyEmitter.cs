using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmitter : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    
    [SerializeField]string emissionOrientationType;
    [SerializeField]int directionOfEmission;

    void Awake(){
        SpawnEnemy(this.enemyPrefab[0], this.emissionOrientationType, this.directionOfEmission);
    }

    void SpawnEnemy(GameObject enemyPrefab, string orientation, int direction){
        GameObject enemySpawned = Instantiate(enemyPrefab,transform.position,transform.rotation);
        EnemyMovement enemySpawnedMovementController = enemySpawned.GetComponent<EnemyMovement>();
        enemySpawnedMovementController.direction = direction;
        enemySpawnedMovementController.typeOfDirection = orientation;
    }
}
