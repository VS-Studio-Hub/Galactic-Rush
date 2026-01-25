using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dailytas : MonoBehaviour
{
    public GameObject dailyTask;
    public GameObject bg;

    public void DailyTask()
    {
        dailyTask.gameObject.SetActive(true);
        bg.gameObject.SetActive(false);
    }

    public void X()
    {
        dailyTask.gameObject.SetActive(false);
        bg.gameObject.SetActive(true);
    }
}
