using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    int playerScore;

    void Awake(){
        playerScore = 0;
    }

    public int GetPlayerScore(){
        return this.playerScore; 
    }

    public void AddScore(int amount){
        this.playerScore += amount;
    }
}