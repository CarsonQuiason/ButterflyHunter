using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    public GameObject butterfly;
    public Canvas menu;
    public Canvas feedBack;
    public Canvas instructions;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnButterfly", 1f, 1f);
        feedBack.enabled = false;
        instructions.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnButterfly()
    {
        float xDir = 0;
        xDir = Random.Range(0, 2) == 0 ? -10 : 10;
        Instantiate(butterfly, new Vector3(xDir, Random.Range(-3, 3), 0), new Quaternion(0, 0, 0, 0));
    }

    public void showFeedback()
    {
        feedBack.enabled = true;
        menu.enabled = false;
        instructions.enabled = false;
    }

    public void goBack()
    {
        menu.enabled = true;
        instructions.enabled = false;
        feedBack.enabled = false;
    }

    public void showInstructions()
    {
        feedBack.enabled = false;
        instructions.enabled = true;
        menu.enabled = false;
    }

    public void playGame()
    {
        SceneManager.LoadScene("SampleScene 1");
    }
}
