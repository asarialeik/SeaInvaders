using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //Singleton

    //Raymond controler
    public bool playing = false;
    public GameObject raymondPrefab;
    public Transform rightBound;
    public Transform leftBound;
    private float spawnHeight = 4f;
    private float spawnInterval = 20f;
    private float raymondSpeed = 5f;

    //PowerUps
    public GameObject[] powerUps;
    public float dropChance = 0.5f;

    //UI
    public GameObject mainMenu;
    public GameObject menu;
    public GameObject title;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject gameOver;
    public GameObject gameOverMenu;
    public GameObject winning;
    public GameObject winningMenu;

    //Score
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    
    public TextMeshProUGUI recordedHighScoreText;
    
    private void Start()
    {

        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        //UI
        mainMenu.SetActive(false);
        mainMenu.SetActive(true);
        gameOver.SetActive(false);
        winning.SetActive(false);

        //Raymond
        InvokeRepeating("SpawnRaymond", spawnInterval, spawnInterval);

        //Score
        score = 0;
    }

    public void Playing()
    {
        playing = true;
    }

    public void AddPoints()
    {
        score = score + 50;
    }

    public void MainMenuStarted()
    {
        LeanTween.moveY(menu.GetComponent<RectTransform>(), -1205f, 0f);
        LeanTween.alpha(title.GetComponent<RectTransform>(), 0, 0f);
        LeanTween.alpha(button1.GetComponent<RectTransform>(), 0, 0f);
        LeanTween.alpha(button2.GetComponent<RectTransform>(), 0, 0f);
        LeanTween.alpha(button3.GetComponent<RectTransform>(), 0, 0f);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);
        LeanTween.moveY(menu.GetComponent<RectTransform>(), 0, 1.5f).setEase(LeanTweenType.easeInOutQuint).setOnComplete(() =>
        {
            LeanTween.alpha(title.GetComponent<RectTransform>(), 1, 1.5f).setEase(LeanTweenType.easeInOutQuint).setOnComplete(() =>
            {
                button1.SetActive(true);
                LeanTween.alpha(button1.GetComponent<RectTransform>(), 1, 1f).setEase(LeanTweenType.easeInCirc).setOnComplete(() =>
                {
                    button2.SetActive(true);
                    LeanTween.alpha(button2.GetComponent<RectTransform>(), 1, 1f).setEase(LeanTweenType.easeInCirc).setOnComplete(() =>
                    {
                        button3.SetActive(true);
                        LeanTween.alpha(button3.GetComponent<RectTransform>(), 1, 1f).setEase(LeanTweenType.easeInCirc).setOnComplete(() =>
                        {
                            button4.SetActive(true);
                            LeanTween.alpha(button4.GetComponent<RectTransform>(), 1, 1f).setEase(LeanTweenType.easeInCirc);
                        });
                    });
                });
            });

        });
    }

    public void GameOver()
    {
        LeanTween.scale(gameOverMenu.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutBack);
        gameOver.SetActive(true);
        playing = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Winning()
    {
        LeanTween.scale(winningMenu.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutBack);
        winning.SetActive(true);
        playing = false;
        scoreText.text = ("Current score: " + score.ToString());
        if (PlayerPrefs.GetInt("High Score") < score)
        {
            PlayerPrefs.SetInt("High Score", score);
            PlayerPrefs.Save();
        }
        highScoreText.text = ("High score: " + PlayerPrefs.GetInt("High Score").ToString());
    }

    void SpawnRaymond()
    {
        //Raymond
        if (playing == true)
        {
            Vector3 spawnPosition = new Vector3(rightBound.position.x + 1f, spawnHeight, 0);

            GameObject raymond = Instantiate(raymondPrefab, spawnPosition, Quaternion.identity);

            RaymondBehavior raymondBehavior = raymond.GetComponent<RaymondBehavior>();
            raymondBehavior.SetLimits(leftBound.position.x, rightBound.position.x);
        }
    }

    public void DestroyRaymond()
    {
        if (Random.value <= dropChance)
        {
            int powerUpIndex = Random.Range(0, powerUps.Length);
            powerUps[powerUpIndex].SetActive(true);
            print("PowerUp " + powerUpIndex);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Records()
    {
        recordedHighScoreText.text = ("High score: " + PlayerPrefs.GetInt("High Score").ToString());
    }
}
