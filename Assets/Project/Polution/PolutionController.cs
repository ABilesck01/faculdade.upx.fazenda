using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolutionController : MonoBehaviour
{
    [SerializeField] private Slider coSlider;
    [SerializeField] private Image visual;
    [SerializeField] private Color goodColor;
    [SerializeField] private Color badColor;
    
    private int CoValue;


    private void Start()
    {
        coSlider.value = 0;
        coSlider.maxValue = 10;
    }

    public void SetCoValue(int value)
    {
        CoValue += value;
        if (CoValue < 0) CoValue = 0;
        visual.color = Color.Lerp(goodColor, badColor, (CoValue / 10));
        coSlider.value = CoValue;
        if (CoValue == 10)
        {
            PlayerMoney.instance.SpendCoins(
                PlayerMoney.instance.GetCoins / 2
            );
        }
    }
}
