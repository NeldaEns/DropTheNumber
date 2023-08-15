using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController ins;
    public List<GameObject> numberBoxSingle;
    public List<GameObject> numberBox;
    public List<GameObject> boxPlay;
    public List<GameObject> objectParent;

    public List<Transform> popup;

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
        SpawnNumber(1, 1);
        boxPlay[0].transform.SetParent(objectParent[2].transform);
        MoveBox();
    }

    public void MoveBox()
    {
        boxPlay[0].transform.DOMove(new Vector3(0, -6, 0), 2);
    }

    private void OnMouseEnter()
    {
        
    }

    public void SpawnNumber(int x, int y)
    {
        int number = Random.Range(1, 6);
        GameObject number1 = Instantiate(numberBoxSingle[number - 1]);
        Vector2 pos = new Vector2(0, 5.45f);
        number1.transform.position = pos;
        boxPlay.Add(number1);
    }
}
