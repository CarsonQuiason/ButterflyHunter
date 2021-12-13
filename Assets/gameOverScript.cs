using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
