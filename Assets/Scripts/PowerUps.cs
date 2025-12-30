using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public Transform player;
    GameObject shield;

    public static bool magnetActive;
    public static bool x2;

    void Start()
    {
        shield = GameObject.FindGameObjectWithTag("PlayerShield");
        shield.SetActive(false);
    }

    void Update()
    {
        if (PowerUpsUIManager.magnetLevelOne)
        {
            Debug.Log("Magnet Level One Active");
        }
        if (PowerUpsUIManager.magnetLevelTwo)
        {
            Debug.Log("Magnet Level Two Active");
        }
        if (PowerUpsUIManager.magnetLevelThree)
        {
            Debug.Log("Magnet Level Three Active");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magnet"))
        {
            Destroy(other.gameObject);
            magnetActive = true;
            StartCoroutine(DisableMagnet(GetDuration("Magnet")));
        }

        if (other.CompareTag("X2"))
        {
            Destroy(other.gameObject);
            x2 = true;
            StartCoroutine(ResetX2(GetDuration("X2")));
        }

        if (other.CompareTag("Shield"))
        {
            Destroy(other.gameObject);
            shield.SetActive(true);
            StartCoroutine(DisableShield(GetDuration("Shield")));
        }
    }
    IEnumerator DisableMagnet(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        magnetActive = false;
    }
    IEnumerator ResetX2(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        x2 = false;
    }
    IEnumerator DisableShield(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        shield.SetActive(false);
    }

    float GetDuration(string type)
    {
        if (type == "Magnet")
        {
            if (PowerUpsUIManager.magnetLevelThree) return 15f;
            if (PowerUpsUIManager.magnetLevelTwo) return 10f;
            return 5f;
        }
        if (type == "Shield")
        {
            if (PowerUpsUIManager.shieldLevelThree) return 15f;
            if (PowerUpsUIManager.shieldLevelTwo) return 10f;
            return 5f;
        }
        if (type == "X2")
        {
            if (PowerUpsUIManager.x2LevelThree) return 15f;
            if (PowerUpsUIManager.x2LevelTwo) return 10f;
            return 5f;
        }
        return 5f;
    }
}
