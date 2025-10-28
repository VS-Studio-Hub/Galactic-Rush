using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterNameManager : MonoBehaviour
{
    public static EnterNameManager instance;

    public TMP_InputField nameInputField;
    private string playerName;

    public TMP_Text PEYN;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PEYN.gameObject.SetActive(false);
        
    }

    public void OnSubmitName()
    {
        //playerName = nameInputField.text;
    }
    public string GetOnSubmitName()
    {
        playerName = nameInputField.text;
        return playerName;
    }
    public void GamePlayScene()
    {
        if(!string.IsNullOrEmpty(nameInputField.text))
        {
            SaveManager.instance.SavePlayerName(nameInputField.text);
            SceneManager.LoadScene("CharacterSelection");
        }
        else
        {
            PEYN.gameObject.SetActive(true);
            PEYN.text = "Please Enter Your Name....!!";
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
