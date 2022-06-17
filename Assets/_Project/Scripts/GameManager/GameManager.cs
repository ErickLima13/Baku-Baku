using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int heigth = 18;
    public static int width = 6;

    public static Transform[,] grid = new Transform[width, heigth];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool InsideGrid(Vector2 pos)
    {
        return (int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0;
    }

    public Vector2 RoundValue(Vector2 nA)
    {
        return new Vector2(Mathf.Round(nA.x), Mathf.Round(nA.y));
    }

    public void UpdateGrid(PiecesController piece)
    {
        for(int y = 0;y < heigth; y++)
        {
            for(int x = 0;x < width; x++)
            {
                if (grid[x,y] != null)
                {
                    if (grid[x, y].parent == piece.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach(Transform block in piece.transform)
        {
            Vector2 pos = RoundValue(block.position);

            if (pos.y < heigth)
            {
                grid[(int)pos.x, (int)pos.y] = block;
            }
        }
    }

    public Transform PosTransformGrid(Vector2 pos)
    {
        if(pos.y > heigth - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }
   

    
}
