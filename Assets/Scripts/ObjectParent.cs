using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParent : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameController.ins.HandleColumnClick(transform);
    }
}