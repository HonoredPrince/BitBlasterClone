using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    int playerScore;
    [SerializeField] GameObject scoreTextPopUpObject = null;
    void Awake(){
        playerScore = 0;
    }

    public int GetPlayerScore(){
        return this.playerScore; 
    }

    public void AddScore(int amount){
        this.playerScore += amount;
    }

    public void SpawnScorePopUpText(Vector3 enemyPosition, int amount){
        GameObject scorePopUpText = Instantiate(scoreTextPopUpObject, enemyPosition, Quaternion.identity);
        TextMeshPro scorePopUpTextComponent = scorePopUpText.GetComponentInChildren<TextMeshPro>();
        scorePopUpTextComponent.text = amount.ToString();
    }
}