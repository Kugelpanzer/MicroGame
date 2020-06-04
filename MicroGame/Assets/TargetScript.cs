using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    protected Grid mainGrid;

    public void SetPos(int x, int y)
    {
        mainGrid = GameObject.Find("MainGrid").GetComponent<Grid>();
        transform.position = mainGrid.CellToWorld(new Vector3Int(x, y, 0));
    }
}
