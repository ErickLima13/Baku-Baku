using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static int height = 18;
    private static int width = 6;

    public Transform[,] grid = new Transform[width, height];

    public List<Food> foodPieces = new();
    public List<Vector2> foodPos = new();

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
            Vector2 pos = RoundValue(block.position); // posição do bloco no grid

            if (pos.y < height)
            {
                grid[(int)pos.x, (int)pos.y] = block;
            }
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

    public void GetFood(Food item)
    {
        foodPieces.Add(item);
    }

    public List<Food> Search(Food item, GameObject animal)
    {
        List<Food> hItems = new() { item };

        int right = (int)animal.transform.position.x + 1;
        int left = (int)animal.transform.position.x - 1;

        int upper = (int)animal.transform.position.y + 1;
        int lower = (int)animal.transform.position.y - 1;

        for (int i = 0; i <= foodPieces.Count; i++)
        {
            for (int z = 0; z <= foodPos.Count; z++)
            {
                while (left >= 0 && grid[left, (int)foodPos[i].y].GetComponent<Food>()._blockColor == animal.GetComponent<Animal>()._blockColor)
                {
                    hItems.Add(grid[left, (int)foodPos[i].y].GetComponent<Food>());
                    left--;
                }

                while (right < width && grid[right, (int)foodPos[i].y].GetComponent<Food>()._blockColor == animal.GetComponent<Animal>()._blockColor)
                {
                    hItems.Add(grid[right, (int)foodPos[i].y].GetComponent<Food>());
                    right++;
                }

                while (lower >= 0 && grid[(int)foodPos[i].x, lower].GetComponent<Food>()._blockColor == animal.GetComponent<Animal>()._blockColor)
                {
                    hItems.Add(grid[(int)foodPos[i].x, lower].GetComponent<Food>());
                    lower--;
                }

                while (upper < height && grid[(int)foodPos[i].x, upper].GetComponent<Food>()._blockColor == animal.GetComponent<Animal>()._blockColor)
                {
                    hItems.Add(grid[(int)foodPos[i].x, upper].GetComponent<Food>());
                    upper++;
                }
            }


        }

        return hItems;
    }

}
