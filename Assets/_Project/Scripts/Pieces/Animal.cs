using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Animal : BlockObject
{
    private GameManager gameManager;

    [SerializeField] private List<BlockObject> foods = new();

    

    void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FillNeighbors();

        if(collision.gameObject.TryGetComponent(out Food  food))
        {
            SearchFood();

            if (food._blockColor == _blockColor)
            {
                food.DestroyNeighbors();
                foods.Clear();
                Destroy(gameObject);
            }
        }
    }

    private void SearchFood()
    {
        List<BlockObject> temporary = new();

        for(int i= 0;i < neighbors.Count; i++)
        {
            if (neighbors[i]._blockColor == _blockColor && neighbors[i]._blockType == BlockType.Food)
            {
                temporary.Add(neighbors[i]);
            }
        }

        foods = temporary.Distinct().ToList();
    }
}
