using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController ins;
    public GameObject numberPrefab;
    public Transform tileContainer;
    public Transform objectParent;
    public int rows = 6;
    public int columns = 5;
    private Box[,] tiles;

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
        InitializeTiles();
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

    void InitializeTiles()
    {
        tiles = new Box[rows, columns];

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                GameObject newTileGO = Instantiate(numberPrefab, tileContainer);
                Box newTile = newTileGO.GetComponent<Box>();
                tiles[i, j] = newTile;
            }
        }
    }

    public void MoveBox(Vector2 direction)
    {
        bool tilesMovedOrMerged = false;

        Vector2Int step = Vector2Int.zero;
        if (direction == Vector2.up) step = Vector2Int.up;
        else if (direction == Vector2.down) step = Vector2Int.down;
        else if (direction == Vector2.left) step = Vector2Int.left;
        else if (direction == Vector2.right) step = Vector2Int.right;

        for (int i = 0; i < rows; i++)
        {
            List<Box> tilesInLine = new List<Box>();

            for (int j = 0; j < columns; j++)
            {
                int rowIndex = (step == Vector2Int.up || step == Vector2Int.down) ? j : i;
                int columnIndex = (step == Vector2Int.left || step == Vector2Int.right) ? j : i;
                Box tile = tiles[rowIndex, columnIndex];

                if (tile != null)
                {
                    tilesInLine.Add(tile);
                }
            }

            List<Box> mergedTiles = new List<Box>();
            for (int j = 0; j < tilesInLine.Count; j++)
            {
                if (tilesInLine[j] == null)
                    continue;

                int targetIndex = j;
                while (targetIndex - 1 >= 0 && tilesInLine[targetIndex - 1] == null)
                {
                    targetIndex--;
                }

                if (targetIndex != j)
                {
                    tilesInLine[targetIndex] = tilesInLine[j];
                    tilesInLine[j] = null;
                    tilesMovedOrMerged = true;
                }

                if (targetIndex - 1 >= 0 && tilesInLine[targetIndex - 1].value == tilesInLine[targetIndex].value &&
                    !mergedTiles.Contains(tilesInLine[targetIndex - 1]))
                {
                    tilesInLine[targetIndex - 1].value *= 2;
                    mergedTiles.Add(tilesInLine[targetIndex - 1]);
                    tilesInLine[targetIndex] = null;
                    tilesMovedOrMerged = true;
                }
            }

            for (int j = 0; j < tilesInLine.Count; j++)
            {
                if (tilesInLine[j] != null)
                {
                    int rowIndex = (step == Vector2Int.up || step == Vector2Int.down) ? j : i;
                    int columnIndex = (step == Vector2Int.left || step == Vector2Int.right) ? j : i;
                    tiles[rowIndex, columnIndex] = tilesInLine[j];
                }
            }
        }

        if (tilesMovedOrMerged)
        {
        }
    }



    public void SpawnNumber()
    {
        GameObject newTile = Instantiate(numberPrefab, tileContainer);
        int newValue = Random.Range(1, 3) * 2;
        newTile.GetComponent<Box>().Initialize(newValue);
        newTile.transform.SetParent(objectParent.transform);
    }
}
