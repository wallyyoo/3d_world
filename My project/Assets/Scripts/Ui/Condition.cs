using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;
    void Start()
    {
        curValue = startValue;
    }

 
    void Update()
    {
        if( uiBar != null)
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Clamp(curValue + value,0, maxValue);
    }


    public void Subtract(float value)
    {
        curValue = Mathf.Clamp(curValue - value,0, maxValue);
    }
}
