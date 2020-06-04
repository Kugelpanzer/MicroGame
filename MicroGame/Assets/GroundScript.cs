using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    protected Grid mainGrid;
    public TowerScript pointer;
    public int x=0, y=0;
    public void SetPos(int x,int y)
    {
        mainGrid = GameObject.Find("MainGrid").GetComponent<Grid>();
        transform.position = mainGrid.CellToWorld(new Vector3Int(x, y, 0));
        this.x = x;
        this.y = y;
    }
    // Start is called before the first frame update

    private void OnMouseDown()
    {
        if (pointer != null)
        {
            pointer.SetTarget(x, y);
        }
    }
}
