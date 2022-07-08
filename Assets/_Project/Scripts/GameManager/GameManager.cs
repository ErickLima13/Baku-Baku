using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private static int height = 14;
    private static int width = 6;

    public Transform[,] grid = new Transform[width, height];

    public int score;

    public float difficulty = 1;

    public bool isPaused;
    public bool isGameOver;

    public int life = 3;

    public List<GameObject> pieces;

    public bool InsideGrid(Vector2 pos)
    {
        return (int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0;
    }

    public Vector2 RoundValue(Vector2 nA)
    {
        return new Vector2(Mathf.RoundToInt(nA.x), Mathf.RoundToInt(nA.y));
    }

    public void UpdateGrid(PiecesController piece)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == piece.transform)
                    {
                        grid[x, y] = null;
                        
                    }
                }
            }
        }

        foreach (Transform block in piece.transform)
        {
            Vector2 pos = RoundValue(block.position);

            if (pos.y < height)
            {
                grid[(int)pos.x, (int)pos.y] = block;
            }
        }
    }
    
    public void UpdateGrid(Transform blockTransform)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y] == blockTransform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }
        
        Vector2 pos = RoundValue(blockTransform.position);

        if (pos.y < height)
        {
            grid[(int)pos.x, (int)pos.y] = blockTransform;
        }
    }

    public Transform PosTransformGrid(Vector2 pos)
    {
        if (pos.y > height - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    public bool AboveGrid(PiecesController piecesController)
    {
        for(int x = 0;x < width; x++)
        {
            foreach (var block in piecesController.Blocks)
            {
                Vector2 pos = RoundValue(block.transform.position);

                if(pos.y > height - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void GameOver()
    {
        life--;
        
        foreach(GameObject g in pieces)
        {
            //Destroy(g);
            g.SetActive(false);
            print("Limpando");
        }

        if(life <= 0)
        {
            print("game over");
            isGameOver = true;
        }
    }

    public void UpdateScore(int value)
    {
        score += value;
    }

}
