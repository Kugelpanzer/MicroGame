using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class WorldScript : MonoBehaviour
{
    public GameObject pesakPrefab;
    public GameObject arabPrefab;
    public static event Action towerAttack;
    public static event Action arabAttack;
    public float timer=1f;
    float currTimer;
    public BaseObject[,] WorldGrid = new BaseObject[4, 4];
    public GroundScript[,] PesakGrid = new GroundScript[4, 4];


    // Start is called before the first frame update
    void Awake()
    {
        currTimer = timer;
        GeneratePesak();
    }
    public void  SetToGrid(BaseObject gj,int x,int y)
    {
        WorldGrid[y, x] = gj;
    }
    public BaseObject GetGrid(int x,int y)
    {
        return WorldGrid[y, x];
    }
    public void Defeat()
    {
        Application.Quit();
    }
    private void GeneratePesak()
    {
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
            {
                GameObject gj=Instantiate(pesakPrefab);
                gj.GetComponent<GroundScript>().SetPos(j, i);
                PesakGrid[i, j] = gj.GetComponent<GroundScript>();
            }
    }

    private void GenerateArabs()
    {
        for (int i = 0; i < 4; i++)
        {
            int r = UnityEngine.Random.Range(0, 4);
            if (r == 0)
            {
                Instantiate(arabPrefab);
                arabPrefab.GetComponent<BaseObject>().x = i;
                arabPrefab.GetComponent<BaseObject>().y = 3;
                //arabPrefab.GetComponent<BaseObject>().SetPos();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currTimer > 0)
        {
            currTimer -= Time.deltaTime;
        }
        else
        {
            towerAttack?.Invoke();
            arabAttack?.Invoke();
            GenerateArabs();
            currTimer = timer;

        }
    }


}
