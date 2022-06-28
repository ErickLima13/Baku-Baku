using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Animal : BlockObject
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.GetInstance();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        FillNeighbors();

    }

    IEnumerator MatchPoint()
    {
        List<BlockObject> matches = new();

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i]._blockType == BlockType.Food && neighbors[i]._blockColor == _blockColor)
            {
                matches.Add(neighbors[i]);
            }
        }

        if (matches.Count >= 1)
        {
            for (int i = 0; i < matches.Count; i++)
            {
                Destroy(matches[i].gameObject);
                gameManager.UpdateScore(matches.Count);

                matches.Clear();

                yield return new WaitForSeconds(0.5f);
                Destroy(gameObject);
            }
        }
        else
        {
            print("NO MATCH");
        }


    }


}
