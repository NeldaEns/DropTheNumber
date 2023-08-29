using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class Box : MonoBehaviour
{
    public int value;
    public Text valueText;
    public Color tileColor;

    public void InitializeTile(int newValue)
    {
        value = newValue;

        valueText.text = value.ToString();

        GetComponent<SpriteRenderer>().color = tileColor;

    }
    public void MoveToPosition(Vector3 targetPosition, float speed)
    {
        transform.DOMove(targetPosition, speed);
    }
}
