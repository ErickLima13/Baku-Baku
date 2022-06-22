using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private static int height = 14;
    private static int width = 6;

    public Transform[,] grid = new Transform[width, height];

    public Food[,] gridFood = new Food[width, height];

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

    public bool AboveGrid(PiecesController piecesController)
    {
        for(int x = 0;x < width; x++)
        {
            foreach (Transform block in piecesController.transform)
            {
                Vector2 pos = RoundValue(block.position);

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
        print("game over");
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

            print(gridFood[left, (int)foodPieces[i].transform.position.y].GetComponent<Food>()._blockColor);




            //while (left >= 0 && grid[left, (int)foodPos[z].y].GetComponent<Food>()._blockColor == animal.GetComponent<Animal>()._blockColor)
            //{
            //    hItems.Add(grid[left, (int)foodPieces[i].transform.position.y].GetComponent<Food>());
            //    left--;
            //}

            //while (right < width && grid[right, (int)foodPos[z].y].GetComponent<Food>()._blockColor == animal.GetComponent<Animal>()._blockColor)
            //{
            //    hItems.Add(grid[right, (int)foodPieces[i].transform.position.y].GetComponent<Food>());
            //    right++;
            //}

            //while (lower >= 0 && grid[(int)foodPos[z].x, lower].GetComponent<Food>()._blockColor == animal.GetComponent<Animal>()._blockColor)
            //{
            //    hItems.Add(grid[(int)foodPieces[i].transform.position.x, lower].GetComponent<Food>());
            //    lower--;
            //}

            //while (upper < height && grid[(int)foodPos[z].x, upper].GetComponent<Food>()._blockColor == animal.GetComponent<Animal>()._blockColor)
            //{
            //    hItems.Add(grid[(int)foodPieces[i].transform.position.x, upper].GetComponent<Food>());
            //    upper++;
            //}


        }

        return hItems;
    }

}
