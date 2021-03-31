using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Splitter : MonoBehaviour
{
    [SerializeField] Transform[] listOfSplittedPartsSpawnPoints = null;
    [SerializeField] GameObject[] listOfSplittedParts = null;

    public void SpawnSplittedParts(){
        int spawnPointsIndex = 0;
        foreach (GameObject splittedPart in listOfSplittedParts){
            int randomSplittedPart = Random.Range(0, listOfSplittedParts.Length);
            Instantiate(listOfSplittedParts[randomSplittedPart], listOfSplittedPartsSpawnPoints[spawnPointsIndex].position, Quaternion.identity);
            spawnPointsIndex++;
        }
    } 
}
