using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Box : MonoBehaviour
{
    public int x;
    public int y;
    public Number number;

    Vector3 firstPos = new Vector3(0.05f, 5.46f);
    float boxSize = 1f;

    public Vector3 CalculatationPosition(int x, int y)
    {
        return new Vector3(firstPos.x + boxSize * x, firstPos.y + boxSize * y, 0);
    }

    public void OnSpawn(int _x, int _y, Number _number)
    {
        x = _x;
        y = _y;
        number = _number;
    }

    public void MoveDown()
    {
        StartCoroutine(MoveCoroutine());

    }

    public void MoveLeft()
    {
        StartCoroutine(MoveCoroutine1());
    }

    public IEnumerator MoveCoroutine1()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.position -= new Vector3(boxSize / 10, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator MoveCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.position -= new Vector3(0, boxSize / 10, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnMouseDown()
    {
        
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