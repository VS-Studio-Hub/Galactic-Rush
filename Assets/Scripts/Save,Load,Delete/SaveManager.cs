using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    //  Profile Handling
    private string GetPlayerKey(string key)
    {
        string playerName = PlayerPrefs.GetString("CurrentPlayerName", "DefaultPlayer");
        return playerName + "_" + key;
    }

    public void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString("CurrentPlayerName", playerName);
        PlayerPrefs.Save();
    }

    public string LoadPlayerName()
    {
        return PlayerPrefs.GetString("CurrentPlayerName", "DefaultPlayer");
    }



    //  Character Selection
    public void SaveCurrentIndex(int index)
    {
        PlayerPrefs.SetInt(GetPlayerKey("SelectedCharacter"), index);
        PlayerPrefs.Save();
    }

    public int LoadCurrentIndex()
    {
        return PlayerPrefs.GetInt(GetPlayerKey("SelectedCharacter"), 0);
    }


    //  Score System
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
        float currentHigh = PlayerPrefs.GetFloat(GetPlayerKey("HighScore"), 0);
        if (score > currentHigh)
        {
            PlayerPrefs.SetFloat(GetPlayerKey("HighScore"), score);
            PlayerPrefs.Save();
        }
    }

    public float LoadHighScore()
    {
        return PlayerPrefs.GetFloat(GetPlayerKey("HighScore"), 0);
    }



    //  Gem System
    public void SaveTotalGem(int gemsToAdd)
    {
        int total = PlayerPrefs.GetInt(GetPlayerKey("TotalGem"), 0);
        total += gemsToAdd;
        PlayerPrefs.SetInt(GetPlayerKey("TotalGem"), total);
        PlayerPrefs.Save();
    }

    /// Sets gems to a specific value
    public void SetTotalGem(int totalGem)
    {
        PlayerPrefs.SetInt(GetPlayerKey("TotalGem"), totalGem);
        PlayerPrefs.Save();
    }

    public int LoadTotalGem()
    {
        return PlayerPrefs.GetInt(GetPlayerKey("TotalGem"), 0);
    }



    //  Character Unlock System
    public void SavePurchasedPlayer(int playerIndex)
    {
        PlayerPrefs.SetInt(GetPlayerKey("Unlocked_" + playerIndex), 1);
        PlayerPrefs.Save();
    }

    public bool LoadPurchasedPlayer(int playerIndex)
    {
        return PlayerPrefs.GetInt(GetPlayerKey("Unlocked_" + playerIndex), 0) == 1;
    }
}
