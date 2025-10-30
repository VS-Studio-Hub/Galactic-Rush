using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public int currentPlayerIndex = 0;
    public GameObject[] players;
    public PlayerBlueprint[] playerBlueprints;

    public TextMeshProUGUI gem;

    public Button play, Buy;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        foreach (PlayerBlueprint blueprint in playerBlueprints)
        {
            if (SaveManager.instance.LoadPurchasedPlayer(blueprint.index))
            {
                blueprint.isPurchased = true;
            }
        }
        foreach (GameObject player in players)
        {
            player.SetActive(false);
        }
        players[currentPlayerIndex].SetActive(true);

        UIUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate();
    }

    void UIUpdate()
    {
        gem.text = SaveManager.instance.LoadTotalGem().ToString();



        if(playerBlueprints[currentPlayerIndex].isPurchased)
        {
            Buy.gameObject.SetActive(false);
            play.gameObject.SetActive(true);
        }
        else
        {
            Buy.gameObject.SetActive(true);
            play.gameObject.SetActive(false);
        }

        int currentGems = SaveManager.instance.LoadTotalGem();
        
        if (currentGems < playerBlueprints[currentPlayerIndex].price && !playerBlueprints[currentPlayerIndex].isPurchased)
        {
            Buy.interactable = false;
        }
    }

    public void BuyPlayer()
    {
        int currentGems = SaveManager.instance.LoadTotalGem();
        if(currentGems >= playerBlueprints[currentPlayerIndex].price && !playerBlueprints[currentPlayerIndex].isPurchased)
        {
            currentGems -= playerBlueprints[currentPlayerIndex].price;
            SaveManager.instance.SetTotalGem(currentGems);
            playerBlueprints[currentPlayerIndex].isPurchased = true;
        }
        SaveManager.instance.SavePurchasedPlayer(playerBlueprints[currentPlayerIndex].index);
    }

    public void NextPlayer()
    {
        players[currentPlayerIndex].SetActive(false);
        currentPlayerIndex++;
        if (currentPlayerIndex == players.Length)
        {
            currentPlayerIndex = 0;
        }
        players[currentPlayerIndex].SetActive(true);
        SaveManager.instance.SavePurchasedPlayer(playerBlueprints[currentPlayerIndex].index);
    }

    public void PreviousPlayer()
    {
        players[currentPlayerIndex].SetActive(false);
        currentPlayerIndex--;
        if (currentPlayerIndex < 0)
        {
            currentPlayerIndex = players.Length - 1;
        }
        players[currentPlayerIndex].SetActive(true);
        SaveManager.instance.SavePurchasedPlayer(playerBlueprints[currentPlayerIndex].index);
    }

    public void Play()
    {
        SceneManager.LoadScene("GamePlay");
        GameManager.gemCount = 0;
    }
}
