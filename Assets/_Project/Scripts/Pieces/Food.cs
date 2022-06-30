using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Food : BlockObject
{
    private GameManager gameManager;

    [SerializeField] private List<BlockObject> sameFoods = new();

    private void Awake()
    {
        Animal.OnNeighborsDestroyed += SeeNeighbors;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FillNeighbors();
        SearchSameFoods();
    }

    private void SearchSameFoods()
    {
        List<BlockObject> temporary = new();

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i]._blockColor == _blockColor && neighbors[i]._blockType == _blockType)
            {
                temporary.Add(neighbors[i]);
            }
        }

        sameFoods = temporary.Distinct().ToList();
    }

    private void SeeNeighbors(Animal animal)
    {
        foreach(BlockObject b in sameFoods)
        {
            animal.foods.Add(b);
        }
    }

    private void OnDestroy()
    {
        Animal.OnNeighborsDestroyed -= SeeNeighbors;
    }
}
