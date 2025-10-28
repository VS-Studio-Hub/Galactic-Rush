using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreDisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreEntryPrefab;
    [SerializeField] private Transform scoreListParent;

    void Start()
    {
        var allScores = ScoreManager.LoadAllScores();
        foreach (var playerData in allScores.playerScores)
        {
            GameObject scoreEntry = Instantiate(scoreEntryPrefab, scoreListParent);
            TMP_Text[] texts = scoreEntry.GetComponentsInChildren<TMP_Text>();
            texts[0].text = playerData.playerName;
            texts[1].text = Mathf.FloorToInt(playerData.highScore) + "m";
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
