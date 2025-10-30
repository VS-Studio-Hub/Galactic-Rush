using UnityEngine;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private void Awake()
    {
        instance = this;
    }
    private string GetPlayerKey(string key)
    {
        string playerName = PlayerPrefs.GetString("CurrentPlayerName", "DefaultPlayer");
        return playerName + " " + key;
    }
    public void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString(GetPlayerKey("PlayerName"), playerName);
        PlayerPrefs.Save();
    }

    public string LoadPlayerName()
    {
        return PlayerPrefs.GetString(GetPlayerKey("PlayerName"), "");
    }

    public void SaveCurrentScore(float score)
    {
        PlayerPrefs.SetFloat(GetPlayerKey("Score"), score);
        PlayerPrefs.Save();
    }

    public float LoadCurrentScore()
    {
        return PlayerPrefs.GetFloat(GetPlayerKey("Score"), 0);
    }

    public void SaveHighScore(float score)
    {
        float currentHighScore = PlayerPrefs.GetFloat(GetPlayerKey("HighScore"), 0);

        if (score > currentHighScore)
        {
            PlayerPrefs.SetFloat(GetPlayerKey("HighScore"), score);
            PlayerPrefs.Save();
        }
    }
    public float LoadHighScore()
    {
        return PlayerPrefs.GetFloat(GetPlayerKey("HighScore"), 0);
    }

    public void SaveGemCollected(int score)
    {
        PlayerPrefs.SetInt(GetPlayerKey("GemCount"), score);
    }
    public int LoadGemCollected()
    {
        return PlayerPrefs.GetInt(GetPlayerKey("GemCount"), 0);
    }

    public void SaveTotalGem(int totalGem)
    {
        int totalGems = PlayerPrefs.GetInt(GetPlayerKey("TotalGem"), 0);
        totalGems += totalGem;
        PlayerPrefs.SetInt(GetPlayerKey("TotalGem"), totalGems);
        PlayerPrefs.Save();
    }

    public void SetTotalGem(int totalGem)
    {
        PlayerPrefs.SetInt(GetPlayerKey("TotalGem"), totalGem);
        PlayerPrefs.Save();
    }


    public int LoadTotalGem()
    {
        return PlayerPrefs.GetInt(GetPlayerKey("TotalGem"), 0);
    }

    public void SavePurchasedPlayer(int playerIndex)
    {
        PlayerPrefs.SetInt(GetPlayerKey("Player_") + playerIndex.ToString(), 1);
        PlayerPrefs.Save();
    }
    public bool LoadPurchasedPlayer(int playerIndex)
    {
        return PlayerPrefs.GetInt(GetPlayerKey("Player_") + playerIndex.ToString(), 0) == 1;
    }

}
