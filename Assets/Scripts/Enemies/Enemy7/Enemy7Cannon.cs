using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7Cannon : MonoBehaviour
{
    [SerializeField] GameObject bomb = null;
    [SerializeField] Transform bombSpawnPoint = null;
    Enemy7Movement enemyMovementHandler;

    void Awake(){
        enemyMovementHandler = GetComponent<Enemy7Movement>();
        InvokeRepeating("SpawnBomb", Random.Range(3f, 5f), Random.Range(4f, 7f));
    }

    void SpawnBomb(){
        StartCoroutine(FireBomb());
    }

    IEnumerator FireBomb(){
        this.enemyMovementHandler.canMove = false;
        yield return new WaitForSeconds(0.5f);
        Instantiate(bomb, bombSpawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        this.enemyMovementHandler.canMove = true;
    }

}
