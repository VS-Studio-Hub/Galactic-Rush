using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode()]
public class EnergyBar : MonoBehaviour
{
    [SerializeField] public float maximum = 100;
    [SerializeField] public float current = 0;
    public Image mask;
    private bool StartDecreasing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
        if (current >= maximum)
        {
            StartDecreasing = true;
        }
        if (current > 0 && StartDecreasing)
        {
            current -= 1;
            if (current == 0)
            {
                StartDecreasing = false;
            }
        }


    }
    void GetCurrentFill()
    {   
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }
}
