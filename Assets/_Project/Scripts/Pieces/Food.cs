using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Food : BlockObject
{
    private GameManager gameManager;

    [SerializeField] private List<BlockObject> sameFoods = new();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FillNeighbors();

        if (collision.gameObject.TryGetComponent(out Animal animal))
        {
            SearchSameFoods();
        }
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

    public void DestroyNeighbors()
    {
        foreach(BlockObject b in sameFoods)
        {
            Destroy(b.gameObject);
        }
    }

    private void OnDestroy()
    {
        DestroyNeighbors();
        gameManager.UpdateScore(1);
    }
}
