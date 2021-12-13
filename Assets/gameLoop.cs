using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Analytics;

public class gameLoop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject butterfly;
    public GameObject bird;
    public GameObject balloon;
    public GameObject whirlwind;
    public GameObject airplane;
    private float timeLeft;
    private float time;
    public static int hits;
    public static int misses;
    public static int level;
    private int hitsNeeded;
    public Text levelText;
    public Text hitsText;
    public Text missesText;
    public Text newLevelText;
    public Text timeText;
    public Text nextLevel;
    private float  birdSpawnRate;
    private float timeSinceLastBird;
    private float balloonSpawnRate;
    private float timeSinceLastBalloon;
    private float airplaneSpawnRate;
    private float timeSinceLastAirplane;
    public Canvas pauseMenu;
    public static bool paused = false;
    public static int userID;

    void Start()
    {
        InvokeRepeating("spawnButterfly", 1f, 5f);
        InvokeRepeating("spawnWhirlwind", 1f, 10f);
        newLevelText.enabled = false;
        pauseMenu.enabled = false;
        userID = PlayerPrefs.GetInt("userID") + 1;
        timeLeft = 45;
        time = 0;
        hits = 0;
        misses = 3;
        level = 1;
        hitsNeeded = 3;
        birdSpawnRate = 4;
        timeSinceLastBird = 0;
        timeSinceLastBalloon = 0;
        balloonSpawnRate = 3;
        airplaneSpawnRate = 5;
        timeSinceLastAirplane = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            time += Time.deltaTime;
            if (time - timeSinceLastBird >= birdSpawnRate)
            {
                Invoke("spawnBird", 0f);
                timeSinceLastBird = time;
            }
            if (time - timeSinceLastBalloon >= balloonSpawnRate)
            {
                Invoke("spawnBalloon", 0f);
                timeSinceLastBalloon = time;
            }

            if (time - timeSinceLastAirplane >= airplaneSpawnRate)
            {
                Invoke("spawnAirplane", 0f);
                timeSinceLastAirplane = time;
            }

            timeLeft -= Time.deltaTime;
            levelText.text = "Level: " + level;
            hitsText.text = "Butterflies: " + hits;
            missesText.text = "Misses: " + misses;
            timeText.text = "Time Left: " + (int)timeLeft;
            nextLevel.text = "Next Level in: " + (hitsNeeded - hits) + " Butterflies";

            if (timeLeft <= 0 || misses == 0)
            {
                levelChange(false);
            }
            else if (hits == hitsNeeded)
            {
                levelChange(true);
            }
        }
    }

    public void levelChange(bool change)
    {
        if(change)
        {
            if(level > 4)
            {
                gameOver();
            }
            else
            {
                level++;
                birdSpawnRate -= .25f;
                balloonSpawnRate -= .25f;
                newLevelText.text = "Level Up!";
                timeLeft += 10;
                StartCoroutine(showNewLevel());
            }
        }
        else
        {
            level--;
            birdSpawnRate += .25f;
            balloonSpawnRate += .25f;
            if (level < 1)
            {
                gameOver();
            }
            else
            {
                newLevelText.text = "Level Down!";
            }
            StartCoroutine(showNewLevel());
        }
        misses = 3;
        hitsNeeded = hits + (level * level);
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

    public void spawnAirplane()
    {
        float xDir = 0;
        xDir = Random.Range(0, 2) == 0 ? -10 : 10;
        Instantiate(airplane, new Vector3(xDir, Random.Range(-5, 5), 0), new Quaternion(0, 0, 0, 0));
    }

    public void pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseMenu.enabled = true;
    }

    public void resume()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.enabled = false;

    }

    public void gameOver()
    {
        if(hits > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", hits);
        }

        string path = Application.dataPath + "/gameLogs/Log.txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Game Log \n\n");
        }
        PlayerPrefs.SetInt("userID", userID);
        string userData = "======USER DATA======\n" + "UserID: " + userID.ToString("0000") + "\nScore: " + hits + "\nDateplayed: " + System.DateTime.Now + "\nDuration: " + AnalyticsSessionInfo.sessionElapsedTime + "\n";
        File.AppendAllText(path, userData);
        SceneManager.LoadScene("GameOver");
    }

}
