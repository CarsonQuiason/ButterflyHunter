using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class gameOverScript : MonoBehaviour
{
    public Text playerScore;
    public Text highScore;
    private int highScoreINT;

    // Start is called before the first frame update
    void Start()
    {
        highScoreINT = PlayerPrefs.GetInt("highScore");
        playerScore.text = "Your Score: " + gameLoop.hits;
        highScore.text = "High score: " + highScoreINT;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAgain()
    {
        SceneManager.LoadScene("SampleScene 1");
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
