using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daily : MonoBehaviour
{
    public int LastDate;

    public int day_1;
    public int day_2;

    public GameObject off_1;
    public GameObject active_1;
    public GameObject check_1;

    public GameObject off_2;
    public GameObject active_2;
    public GameObject check_2;

    private void Start()
    {
        day_1 = PlayerPrefs.GetInt("day_1");
        day_2 = PlayerPrefs.GetInt("day_2");
        LastDate = PlayerPrefs.GetInt("LastDate");

        Reward();

        if(LastDate != System.DateTime.Now.Day)
        {
            if (day_1 == 0)
            {
                day_1 = 1;
            }
            else if (day_2 == 0)
            {
                day_2 = 1;
            }

            Reward();
        }
    }

    public void Reward()
    {
        if(day_1 == 1)
        {
            off_1.SetActive(true);
            active_1.SetActive(false);
            check_1.SetActive(false);
        }
        if(day_1 == 1)
        {
            off_1.SetActive(false);
            active_1.SetActive(true);
            check_1.SetActive(false);
        }
        if(day_1 == 2)
        {
            off_1.SetActive(false);
            active_1.SetActive(false);
            check_1.SetActive(true);
        }

        if(day_2 == 1)
        {
            off_2.SetActive(true);
            active_2.SetActive(false);
            check_2.SetActive(false);
        }
        if(day_2 == 1)
        {
            off_2.SetActive(false);
            active_2.SetActive(true);
            check_2.SetActive(false);
        }
        if(day_2 == 2)
        {
            off_2.SetActive(false);
            active_2.SetActive(false);
            check_2.SetActive(true);
        }
    }

    public void GetReward_1()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDate", LastDate);

        print("Reward 1");

        day_1 = 2;
        PlayerPrefs.SetInt("day_1", 2);

        Reward();
    }
    public void GetReward_2()
    {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDate", LastDate);

        print("Reward 2");

        day_2 = 2;
        PlayerPrefs.SetInt("day_2", 2);

        Reward();
    }
}
