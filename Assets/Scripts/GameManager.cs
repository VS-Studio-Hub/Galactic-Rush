using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Menu Settings")]
    [SerializeField] private GameObject menu;
    [SerializeField] private TMP_Text highScore;
    [SerializeField] private TMP_Text score;
    private float highScoreValue;

    [Header("Lane Speed Settings")]
    public float minSpeed = -5f;
    public float maxSpeed = -15f;
    public float accelerationDistance = 500f; // how far until max speed is reached
    public float smoothFactor = 2f; // controls smooth transition speed

    private float currentSpeed;
    private float distanceTraveled = 0f;
    [SerializeField] private TMP_Text distanceText;

    [Header("Gem Settings")]
    public static int gemCount;
    [SerializeField] private TMP_Text gemCollector;

    private string playerName;


    public static bool gameOver = false;
    private bool scoreSaved = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentSpeed = minSpeed;
        menu.SetActive(false);

        playerName = SaveManager.instance.LoadPlayerName();
        highScoreValue = SaveManager.instance.LoadHighScore();
    }

    void Update()
    {
        if (gameOver)
        {
            if (!scoreSaved)
            {
                menu.SetActive(true);
                //Time.timeScale = 0f;

                SaveManager.instance.SaveCurrentScore(distanceTraveled);
                SaveManager.instance.SaveHighScore(distanceTraveled);
                SaveManager.instance.SaveTotalGem(gemCount);

                ScoreManager.SaveScore(playerName, distanceTraveled);

                highScoreValue = SaveManager.instance.LoadHighScore();

                UpdateUI();
                scoreSaved = true;
            }
            return;
        }
        //distanceText.text = playerName + Mathf.FloorToInt(distanceTraveled) + "m";
        LaneSpeed();
        UpdateUI();
    }

    private void UpdateUI()
    {
        distanceText.text = $"{playerName}: {Mathf.FloorToInt(distanceTraveled)}m";

        gemCollector.text = gemCount.ToString();

        //gemCollector.text = $"Gems: {gemCount}";

        highScore.text = $"{Mathf.FloorToInt(highScoreValue)}m";

        score.text = $"{Mathf.FloorToInt(distanceTraveled)}m";
    }

    private void LaneSpeed()
    {
        distanceTraveled += -currentSpeed * Time.deltaTime;

        if (PowerUps.x2)
        {
            distanceTraveled += (-currentSpeed * 2f) * Time.deltaTime ;
        }

        //Debug.Log("Distance Traveled: " + Mathf.FloorToInt(distanceTraveled));

        float t = Mathf.Clamp01(distanceTraveled / accelerationDistance);
        float targetSpeed = Mathf.Lerp(minSpeed, maxSpeed, t);

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smoothFactor);
    }


    public float GetDistanceTraveled()
    {
        return distanceTraveled;
    }

    public float MoveSpeed()
    {
        return currentSpeed;
    }

    public void RestartGame()
    {
       // Time.timeScale = 1f; 
        SceneManager.LoadScene("GamePlay");
        gameOver = false;
        gemCount = 0;
        scoreSaved = false;
    }

    public void BackToMainMenu()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        gameOver = false;
        scoreSaved = false;
    }

    public void HomeMenu()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene("CharacterSelection");
        gameOver = false;
        scoreSaved = false;
    }
}