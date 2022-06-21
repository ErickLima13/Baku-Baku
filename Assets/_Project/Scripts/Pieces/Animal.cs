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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Food food) && food._blockColor == _blockColor)
        {
            StartCoroutine(MatchPoint(food));
        }
    }

    IEnumerator MatchPoint(Food food)
    {
        List<Food> matches = SearchHorizontally(food);

        if (matches.Count >= 1)
        {
            for (int i = 0; i < matches.Count; i++)
            {
                Destroy(matches[i].gameObject);
                gameManager.foodPieces.Remove(food);
                matches.Clear();
            }
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    List<Food> SearchHorizontally(Food item)
    {
        List<Food> hItems = new() { item };

        if(gameManager.foodPieces != null)
        {
            for (int i = 0; i < gameManager.foodPieces.Count; i++)
            {
                if (_blockColor == gameManager.foodPieces[i].GetComponent<Food>()._blockColor)
                {
                    hItems.Add(gameManager.foodPieces[i].GetComponent<Food>());
                }
            }
        }

        return hItems;
    }

}
