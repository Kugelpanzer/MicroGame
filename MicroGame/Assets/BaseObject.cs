using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public int x=0, y=0;
    public int health = 1;
    public int damage = 1; 
    public Type type;
    public List<Type> targets = new List<Type>();

    protected Grid mainGrid;
    protected WorldScript controller;


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathTrigger();
        }
    }

    public void DeathTrigger()
    {
        controller.WorldGrid[y, x] = null;
        
        Debug.Log(type);
        Unsub();
        if (gameObject != null)
        {  
            Destroy(gameObject);
        }

    }
    protected virtual void Unsub()
    {

    }
    // Start is called before the first frame update
    protected void Awake()
    {
        GameObject gj = GameObject.Find("MainGrid");
        mainGrid = gj.GetComponent<Grid>();
        controller = gj.GetComponent<WorldScript>();
        SetPos();
        controller.SetToGrid(this, x, y);
    }
    protected void SetPos()
    {
        transform.position = mainGrid.CellToWorld(new Vector3Int(x, y, 0));
    }
    // Update is called once per frame
    protected void Update()
    {
        
    }
}
