using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public int value;
    public Text valueText;
    public Color tileColor;

    private bool isMoving = false;
    private Vector3 targetPosition;
    private float moveSpeed = 5f;

    public void InitializeTile(int newValue, Color newColor)
    {
        value = newValue;
        tileColor = newColor;

        GetComponent<SpriteRenderer>().color = tileColor;

        valueText.text = value.ToString();
    }

    public void MoveToPosition(Vector3 newPosition)
    {
        if (!isMoving)
        {
            targetPosition = newPosition;
            StartCoroutine(MoveCoroutine());
        }
    }

    private IEnumerator MoveCoroutine()
    {
        isMoving = true;
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
        isMoving = false;
    }

}
