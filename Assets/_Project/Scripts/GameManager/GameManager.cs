using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static int height = 18;
    private static int width = 6;

    public Transform[,] grid = new Transform[width, height];

    public List<Food> foodPieces = new();

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
        for(int y = 0;y < height; y++)
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

            if (pos.y < height)
            {
                grid[(int)pos.x, (int)pos.y] = block;
            }
        }
    }

    public Transform PosTransformGrid(Vector2 pos)
    {
        if(pos.y > height - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        } 
    }
    
    public void GetFood(Food item)
    {
        foodPieces.Add(item);
    }

}
