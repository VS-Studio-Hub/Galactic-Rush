using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    private Vector3 targetScale = new Vector3(1f, 1f, 1f);
    private float duration = 1f;
    private float elapsedTime = 0f;
    private Vector3 startScale;
    void Start()
    {
        float random = Random.Range(0.1f, 1f);
        startScale = new Vector3(random, random, random);
        transform.localScale = startScale;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
        }
    }
}
