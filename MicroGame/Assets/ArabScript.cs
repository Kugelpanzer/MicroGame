using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArabScript : BaseObject
{

    private void TryMove()
    {
        if (y > 0)
        {
            BaseObject bo = controller.GetGrid(x, y - 1);
            if ( bo == null)
            {
                Move();
            }
            else if (targets.Contains(bo.type))
            {
                Attack(bo);
            }
        }
        else
        {
            controller.Defeat();
        }
    }
    private void Move()
    {
            y--;
            SetPos();
    }
    private void Attack(BaseObject b)
    {
        Debug.Log("arab attack");
        b.TakeDamage(damage);
        DeathTrigger();
    }

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        WorldScript.arabAttack += TryMove;
    }
    protected override void Unsub()
    {
        WorldScript.arabAttack -= TryMove;
    }
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
