using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public int value;
    public Text valueText;

    public void Initialize(int newValue)
    {
        value = newValue;
        UpdateAppearance();
    }

    void UpdateAppearance()
    {
        valueText.text = value.ToString();
    }
}

public enum Number
{
    None = 0,
    Two,
    Four,
    Eight,
    One6,
    Three2,
    Six4,
    One28,
    Two56,
    Five12,
    One024,
    Two048,
    Four096,
    Eight192,
    One6384
}