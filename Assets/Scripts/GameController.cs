using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController ins;
    public GameObject numberPrefab;
    public Transform tileContainer;

    private void Awake()
    {
        if(ins != null)
        {
            Destroy(gameObject);
        }
        else
        {
            ins = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SpawnNumber();
        SpawnNumber();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveBox(Vector2.left);
            SpawnNumber();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveBox(Vector2.right);
            SpawnNumber();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveBox(Vector2.up);
            SpawnNumber();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveBox(Vector2.down);
            SpawnNumber();
        }
    }

    public void MoveBox(Vector2 direction)
    {
    }



    public void SpawnNumber()
    {
        GameObject newTile = Instantiate(numberPrefab, tileContainer);
        int newValue = Random.Range(1, 3) * 2;
        newTile.GetComponent<Box>().Initialize(newValue);
    }
}
