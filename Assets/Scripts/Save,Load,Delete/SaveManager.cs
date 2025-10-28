using UnityEngine;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //playerName = EnterNameManager.instance.GetOnSubmitName();
    }
    public void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
    }

    public string LoadPlayerName()
    {
        return PlayerPrefs.GetString("PlayerName", "");
    }

    public void SaveCurrentScore(float score)
    {
        PlayerPrefs.SetFloat("Score", score);
        PlayerPrefs.Save();
    }

    public float LoadCurrentScore()
    {
        return PlayerPrefs.GetFloat("Score", 0);
    }

    public void SaveHighScore(float score)
    {
        float currentHighScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (score > currentHighScore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
            PlayerPrefs.Save();
        }
    }
    public float LoadHighScore()
    {
        return PlayerPrefs.GetFloat("HighScore", 0);
    }

    public void SaveGemCollected(int score)
    {
        PlayerPrefs.SetInt("GemCount", score);
    }
    public int LoadGemCollected()
    {
        return PlayerPrefs.GetInt("GemCount", 0);
    }

    public void SaveTotalGem(int totalGem)
    {
        int totalGems = PlayerPrefs.GetInt("TotalGem", 0);
        totalGems += totalGem;
        PlayerPrefs.SetInt("TotalGem", totalGems);
        PlayerPrefs.Save();
    }


    //public void SaveTotalGem(int Gem)
    //{
    //    PlayerPrefs.SetInt("TotalGem", PlayerPrefs.GetInt("TotalGem", 0) + LoadGemCollected());
    //}

    public int LoadTotalGem()
    {
        return PlayerPrefs.GetInt("TotalGem", 0);
    }
}
