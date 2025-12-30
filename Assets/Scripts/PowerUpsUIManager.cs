using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpsUIManager : MonoBehaviour
{
    public static bool magnetLevelOne, magnetLevelTwo, magnetLevelThree;
    public static bool shieldLevelOne, shieldLevelTwo, shieldLevelThree;
    public static bool x2LevelOne, x2LevelTwo, x2LevelThree;

    int currentMagnetLevel, currentShieldLevel, currentX2Level;

    public TMP_Text magnetLevel, shieldLevel, x2Level;

    public Button magnetBtn, shieldBtn, x2Btn;

    void Start()
    {
        currentMagnetLevel = 0;
        currentShieldLevel = 0;
        currentX2Level = 0;
        magnetLevelOne = false;
        magnetLevelTwo = false;
        magnetLevelThree = false;
        shieldLevelOne = false;
        shieldLevelTwo = false;
        shieldLevelThree = false;
        x2LevelOne = false;
        x2LevelTwo = false;
        x2LevelThree = false;
        magnetLevel.text = "LEVEL - " + currentMagnetLevel;
        shieldLevel.text = "LEVEL - " + currentMagnetLevel;
        x2Level.text = "LEVEL - " + currentMagnetLevel;
    }

    void Update()
    {
        
    }

    public void LevelUpMagnet()
    {
        currentMagnetLevel++;
        if(currentMagnetLevel == 1)
        {
            magnetLevelOne = true;
            magnetLevelTwo = false;
            magnetLevelThree = false;
        }
        else if(currentMagnetLevel == 2)
        {
            magnetLevelOne = false;
            magnetLevelTwo = true;
            magnetLevelThree = false;
        }
        else if(currentMagnetLevel == 3)
        {
            magnetLevelOne = false;
            magnetLevelTwo = false;
            magnetLevelThree = true;
        }
        magnetLevel.text = "LEVEL - " + currentMagnetLevel;

        if(currentMagnetLevel == 3)
        {
            magnetBtn.interactable = false;
        }
    }

    public void LevelUpShield()
    {
        currentShieldLevel++;
        if (currentShieldLevel == 1)
        {
            shieldLevelOne = true;
            shieldLevelTwo = false;
            shieldLevelThree = false;
        }
        else if (currentShieldLevel == 2)
        {
            shieldLevelOne = false;
            shieldLevelTwo = true;
            shieldLevelThree = false;
        }
        else if (currentShieldLevel == 3)
        {
            shieldLevelOne = false;
            shieldLevelTwo = false;
            shieldLevelThree = true;
        }
        shieldLevel.text = "LEVEL - " + currentShieldLevel;
        if (currentShieldLevel == 3)
        {
            shieldBtn.interactable = false;
        }
    }

    public void LevelUpX2()
    {
        currentX2Level++;
        if (currentX2Level == 1)
        {
            x2LevelOne = true;
            x2LevelTwo = false;
            x2LevelThree = false;
        }
        else if (currentX2Level == 2)
        {
            x2LevelOne = false;
            x2LevelTwo = true;
            x2LevelThree = false;
        }
        else if (currentX2Level == 3)
        {
            x2LevelOne = false;
            x2LevelTwo = false;
            x2LevelThree = true;
        }
        x2Level.text = "LEVEL - " + currentX2Level;
        if (currentX2Level == 3)
        {
            x2Btn.interactable = false;
        }
    }
}