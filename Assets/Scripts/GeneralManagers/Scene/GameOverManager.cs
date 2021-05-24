using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject newHighScoreTextObject = null, currentHighScoreTextObject = null;
    [SerializeField] TMP_Text personalBestTxt = null, currentHighScoreTxt = null;
    void Awake(){
        CheckHighScore();
    } 

    void CheckHighScore(){
        if(PlayerPrefs.GetInt("PlayerHighScore") > PlayerPrefs.GetInt("PlayerCurrentHighScore")){
            PlayerPrefs.SetInt("PlayerCurrentHighScore", PlayerPrefs.GetInt("PlayerHighScore"));
            PlayerPrefs.Save();
            ShowPersonalBestHighScore();
        }else{
            ShowCurrentHighScore();
        }
    }

    void ShowPersonalBestHighScore(){
        newHighScoreTextObject.SetActive(true);
        currentHighScoreTextObject.SetActive(false);

        personalBestTxt.text = "Highscore: " + PlayerPrefs.GetInt("PlayerCurrentHighScore").ToString();
    }
    
    void ShowCurrentHighScore(){
        newHighScoreTextObject.SetActive(false);
        currentHighScoreTextObject.SetActive(true);

        currentHighScoreTxt.text = PlayerPrefs.GetInt("PlayerCurrentHighScore").ToString();
    }
}
