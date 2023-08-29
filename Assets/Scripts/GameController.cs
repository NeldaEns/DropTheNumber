using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController ins;
    public GameObject tilePrefab;
    public List<Transform> columns;
    public Transform targetColumn;
    private Box movingTile;
    public float moveSpeed = 1.0f;
    private int[] possibleValues = { 2, 4, 8, 16, 32 };


    public LayerMask tileLayerMask;

    private void Awake()
    {
        if (ins != null)
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
        SpawnNewTile();
    }

    public void SpawnNewTile()
    {
        int randomIndex = Random.Range(0, possibleValues.Length);
        int randomValue = possibleValues[randomIndex];

        Transform spawnColumn = columns[Random.Range(0, columns.Count)];

        Vector3 spawnPosition = new Vector3(spawnColumn.position.x, 6 , spawnColumn.position.z);

        GameObject newNumberTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
        Box numberTileComponent = newNumberTile.GetComponent<Box>();
        numberTileComponent.transform.SetParent(spawnColumn.transform);
        if (numberTileComponent != null)
        {
            switch (randomValue)
            {
                case 2:
                    numberTileComponent.tileColor = Color.red;
                    break;

                case 4:
                    numberTileComponent.tileColor = Color.yellow;
                    break;

                case 8:
                    numberTileComponent.tileColor = Color.blue;
                    break;

                case 16:
                    numberTileComponent.tileColor = Color.green;
                    break;

                case 32:
                    numberTileComponent.tileColor = Color.cyan;
                    break;
            }
            if(numberTileComponent.value > 32)
            {
                numberTileComponent.tileColor = Color.gray;
            }
            numberTileComponent.InitializeTile(randomValue);

            StartCoroutine(MoveTileDown(numberTileComponent, spawnColumn));
        }
        else
        {
            Debug.LogWarning("NumberTile component not found on the prefab.");
        }
    }


    private IEnumerator MoveTileDown(Box tile, Transform spawnColumn)
    {
        float endY = GetEmptyRowForColumn(spawnColumn) - 5.5f;
        Vector3 targetPosition = new Vector3(tile.transform.position.x, endY, tile.transform.position.z);
        float startY = tile.transform.position.y;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            float newY = Mathf.Lerp(startY, targetPosition.y, t);
            tile.transform.position = new Vector3(tile.transform.position.x, newY, tile.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }

        tile.transform.position = targetPosition;
    }


    private int GetEmptyRowForColumn(Transform column)
    {
        int emptyRow = 0;

        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(column.position.x, 6), Vector2.down, 10f, tileLayerMask);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("NumberTile"))
            {
                emptyRow = Mathf.Min(emptyRow, Mathf.FloorToInt(hit.point.y) - 1);
            }
        }

        return emptyRow;
    }

    public void HandleColumnClick(Transform clickedColumn)
    {
        targetColumn = clickedColumn;
        if (movingTile != null && targetColumn != null)
        {
            MoveTileToColumn();
        }
    }

    private void MoveTileToColumn()
    {
        Vector3 targetPosition = new Vector3(targetColumn.position.x, movingTile.transform.position.y, targetColumn.position.z);
        movingTile.MoveToPosition(targetPosition, moveSpeed);
        movingTile = null;
        targetColumn = null;
    }

}
