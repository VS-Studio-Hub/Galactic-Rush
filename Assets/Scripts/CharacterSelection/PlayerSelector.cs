using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{

    public int currentPlayerIndex = 0;
    public GameObject[] players;
    public static GameObject player;

    void Start()
    {
        //currentPlayerIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);
        //foreach (GameObject player in players)
        //{
        //    player.SetActive(false);
        //}
        currentPlayerIndex = SaveManager.instance.LoadCurrentIndex();
        //players[currentPlayerIndex].SetActive(true);
        Vector3 spawnPosition = new Vector3(0, .183f, 50);
        Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);
        player = Instantiate(players[currentPlayerIndex], spawnPosition, spawnRotation);
    }
}
