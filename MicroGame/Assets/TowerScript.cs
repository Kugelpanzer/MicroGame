using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TowerScript : BaseObject
{
    [Range(0,3)]
    public int xMin, xMax, yMin, yMax;
    public Point prefferedTarget;
    public TargetScript target;
    public GameObject targetPrefab;
    protected void Attack(BaseObject b)
    {
        Debug.Log("Tower attack");
            b.TakeDamage(damage);
    }
    protected void TryAttack()
    {
        if (prefferedTarget != null
            && controller.GetGrid(prefferedTarget.x, prefferedTarget.y) != null
            && targets.Contains(controller.GetGrid(prefferedTarget.x, prefferedTarget.y).type)
            )
        {
            Attack(controller.GetGrid(prefferedTarget.x, prefferedTarget.y));
        }
        else// if(prefferedTarget != null)//ovo izbaci ako hoces da uvek puca kada ima nekog neprijatelja u rangu 
        {

            List<BaseObject> bo = new List<BaseObject>();
            for (int i = yMin; i < yMax + 1; i++)
                for (int j = xMin; j < xMax + 1; j++)
                {
                    if (controller.GetGrid(j, i) != null && targets.Contains(controller.GetGrid(j, i).type))
                    {
                        bo.Add(controller.GetGrid(j, i));
                    }
                }
            bo = bo.OrderBy(i => Guid.NewGuid()).ToList();

            if (bo.Count > 0 && bo[0] !=null)
            {
                
                Attack(bo[0]);
            }
        }
    }

    protected override void Unsub()
    {
        WorldScript.towerAttack -= TryAttack;
    }
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        WorldScript.towerAttack += TryAttack;
        /*List<string> listOfThings = new List<string>()
        {
        "1", "2", "3", "4"
        };
        // Randomly Order it by Guid..
        listOfThings = listOfThings.OrderBy(i => Guid.NewGuid()).ToList();*/
    }
    public void SetTarget(int x,int y)
    {
        if (prefferedTarget == null)
        {
            prefferedTarget = new Point(x, y);
            target = Instantiate(targetPrefab).GetComponent<TargetScript>();
            target.SetPos(x, y);
        }
        else
        {
            if(x==prefferedTarget.x && y == prefferedTarget.y)
            {
                Destroy(target.gameObject);
                prefferedTarget = null;
            }
            else
            {
                prefferedTarget = new Point(x, y);
                target.SetPos(x, y);
            }
        }
    }
    private void Start()
    {
        for (int i = yMin; i < yMax+1 ; i++)
            for (int j = xMin; j < xMax+1 ; j++)
            {

                controller.PesakGrid[i,j].pointer = this;
            }
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
