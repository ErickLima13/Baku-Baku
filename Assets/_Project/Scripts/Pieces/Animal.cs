using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Animal : BlockObject
{
    private GameManager gameManager;

    [SerializeField] public List<BlockObject> foods = new();

    public delegate void SearchNeighbors(Animal animal);
    public static event SearchNeighbors OnNeighborsDestroyed;

    void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FillNeighbors();
        SearchFood();

        if (collision.gameObject.TryGetComponent(out Food food))
        {
            OnNeighborsDestroyed?.Invoke(this);

            //if(food._blockColor == _blockColor)
            //{
            //    foreach (BlockObject b in foods)
            //    {
            //        Destroy(b.gameObject);
            //    }
            //}
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        List<BlockObject> temporary = new();

        for (int i = 0; i < foods.Count; i++)
        {
            if (foods[i]._blockColor == _blockColor)
            {
                temporary.Add(foods[i]);
            }
        }

        foods = temporary.Distinct().ToList();
    }

    private void SearchFood()
    {
        List<BlockObject> temporary = new();

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i]._blockColor == _blockColor && neighbors[i]._blockType == BlockType.Food)
            {
                temporary.Add(neighbors[i]);
            }
        }

        foods = temporary.Distinct().ToList();
    }
}
