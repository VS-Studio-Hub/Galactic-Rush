using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScoreData
{
    public string playerName;
    public float highScore;
}

[System.Serializable]
public class AllScoreData
{
    public List<PlayerScoreData> playerScores = new List<PlayerScoreData>();
}

public static class ScoreManager
{
    private const string scoreKey = "AllPlayerData";

    public static void SaveScore(string playerName, float score)
    {
        AllScoreData allScores = LoadAllScores();

        PlayerScoreData existingPlayer = null;

        foreach (PlayerScoreData playerData in allScores.playerScores)
        {
            if (playerData.playerName == playerName)
            {
                existingPlayer = playerData;
                break;
            }
        }
        if (existingPlayer != null)
        {
            if (score > existingPlayer.highScore)
            {
                existingPlayer.highScore = score;
            }

            allScores.playerScores.Remove(existingPlayer);
            allScores.playerScores.Insert(0, existingPlayer);
        }
        else
        {
            PlayerScoreData newPlayerData = new PlayerScoreData
            {
                playerName = playerName,
                highScore = score
            };
            allScores.playerScores.Insert(0, newPlayerData);
        }

        string json = JsonUtility.ToJson(allScores);
        PlayerPrefs.SetString(scoreKey, json);
        PlayerPrefs.Save();
    }
    
    public static AllScoreData LoadAllScores()
    {
        if (PlayerPrefs.HasKey(scoreKey))
        {
            string json = PlayerPrefs.GetString(scoreKey);
            return JsonUtility.FromJson<AllScoreData>(json);
        }
        return new AllScoreData();
    }
}