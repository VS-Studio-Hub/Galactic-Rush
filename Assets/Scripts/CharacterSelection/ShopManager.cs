using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int currentPlayerIndex;
    public GameObject[] playerPrefabs;

    public PlayerBlueprint[] playerBlueprints;
    public Button buyButton;
    public Button startButton;

    public TextMeshProUGUI coinText;
    void Start()
    {
        coinText.text = "Coins: " + SaveManager.instance.LoadTotalGem();
        foreach (PlayerBlueprint player in playerBlueprints)
        {
            if(player.price == 0)
                player.isPurchased = true;
            else
                player.isPurchased = PlayerPrefs.GetInt(player.name, 0) == 0 ? false:true;
        }
        currentPlayerIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);
        foreach (GameObject player in playerPrefabs)
        {
            player.SetActive(false);
        }

        playerPrefabs[currentPlayerIndex].SetActive(true);

    }
    void Update()
    {
        UpdateUI();
    }

    public void NextPlayer()
    {
        playerPrefabs[currentPlayerIndex].SetActive(false);

        currentPlayerIndex++;
        if (currentPlayerIndex == playerPrefabs.Length)
        {
            currentPlayerIndex = 0;
        }
        playerPrefabs[currentPlayerIndex].SetActive(true);
        PlayerBlueprint p = playerBlueprints[currentPlayerIndex];
        if (!p.isPurchased)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
    }

    public void PreviousPlayer()
    { 
        playerPrefabs[currentPlayerIndex].SetActive(false);

        currentPlayerIndex--;
        if (currentPlayerIndex == -1)
        {
            currentPlayerIndex = playerPrefabs.Length - 1;
        }
        playerPrefabs[currentPlayerIndex].SetActive(true);
        PlayerBlueprint p = playerBlueprints[currentPlayerIndex];
        if (!p.isPurchased)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
    }

    public void UnlockPlayer()
    {
        PlayerBlueprint p = playerBlueprints[currentPlayerIndex];

        PlayerPrefs.SetInt(p.name, 1);
        PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
        p.isPurchased = true;
        SaveManager.instance.SaveTotalGem(SaveManager.instance.LoadTotalGem() - p.price);
        //SaveManager.instance.SaveGemCount(PlayerPrefs.GetInt("GemCount", 0) - p.price);
        buyButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    private void UpdateUI()
    {
        PlayerBlueprint p = playerBlueprints[currentPlayerIndex];
        if (p.isPurchased)
        {
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy- " + p.price;
            if (p.price > SaveManager.instance.LoadTotalGem())
            {
                buyButton.interactable = false;
            }
            else
            {
                buyButton.interactable = true;
            }
        }
    }

    public void GamePlay()
    {
        GameManager.gemCount = 0;
        SceneManager.LoadScene("GamePlay");
    }
}
