using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameLoop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject butterfly;
    public GameObject bird;
    public GameObject balloon;
    public GameObject whirlwind;
    private float timeLeft = 25;
    private float time = 0;
    static public int hits = 0;
    static public int misses = 3;
    private int level = 1;
    private int hitsNeeded = 3;
    public Text levelText;
    public Text hitsText;
    public Text missesText;
    public Text newLevelText;
    public Text timeText;
    private float  birdSpawnRate = 4;
    private float timeSinceLastBird = 0;
    private float balloonSpawnRate = 3;
    private float timeSinceLastBalloon = 0;

    void Start()
    {
        InvokeRepeating("spawnButterfly", 1f, 5f);
        InvokeRepeating("spawnWhirlwind", 1f, 10f);
        newLevelText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time - timeSinceLastBird >= birdSpawnRate)
        {
            Invoke("spawnBird", 0f);
            timeSinceLastBird = time;
        }
        if(time - timeSinceLastBalloon >= balloonSpawnRate)
        {
            Invoke("spawnBalloon", 0f);
            timeSinceLastBalloon = time;
        }

        timeLeft -= Time.deltaTime;
        levelText.text = "Level: " + level;
        hitsText.text = "Butterflies: " + hits;
        missesText.text = "Misses: " + misses;
        timeText.text = "Time Left: " + (int)timeLeft;

        if(timeLeft <= 0 || misses == 0)
        {
            levelChange(false);
        }
        else if(hits == hitsNeeded * level)
        {
            levelChange(true);
        }


    }

    public void levelChange(bool change)
    {
        if(change)
        {
            level++;
            birdSpawnRate -= .25f;
            balloonSpawnRate -= .25f;
            newLevelText.text = "Level Up!";
            StartCoroutine(showNewLevel());
        }
        else
        {
            level--;
            birdSpawnRate += .25f;
            balloonSpawnRate += .25f;
            if (level < 1)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                newLevelText.text = "Level Down!";
            }
            StartCoroutine(showNewLevel());
        }
        timeLeft = level * 25;
        hits = 0;
        misses = 3;
    }
    IEnumerator showNewLevel()
    {
        newLevelText.enabled = true;
        yield return new WaitForSeconds(1.5f);
        newLevelText.enabled = false;
    }

    public void spawnButterfly()
    {
        float xDir = 0;
        xDir = Random.Range(0, 2) == 0 ? -10 : 10;
        Instantiate(butterfly, new Vector3(xDir, Random.Range(-3, 3), 0), new Quaternion(0, 0, 0, 0));
    }

    public void spawnBird()
    {
        float xDir = 0;
        xDir = Random.Range(0, 2) == 0 ? -10 : 10;
        Instantiate(bird, new Vector3(xDir, Random.Range(-5, 5), 0), new Quaternion(0, 0, 0, 0));
    }

    public void spawnBalloon()
    {
        float xDir = 0;
        xDir = Random.Range(-7, 7);
        Instantiate(balloon, new Vector3(xDir, -5, 0), new Quaternion(0, 0, 0, 0));
    }

    public void spawnWhirlwind()
    {
        Instantiate(whirlwind, new Vector3(Random.Range(-5, 5), Random.Range(-3, 6), 0), new Quaternion(0, 0, 0, 0));
    }

}
