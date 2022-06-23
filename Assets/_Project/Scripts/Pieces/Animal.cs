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
            //StartCoroutine(MatchPoint(food));
            gameManager.UpdateScore(1);
        }
    }

    IEnumerator MatchPoint(Food food)
    {
        List<Food> matches = gameManager.Search(food,this.gameObject);

        if (matches.Count >= 1)
        {
            for (int i = 0; i < matches.Count; i++)
            {
                Destroy(matches[i].gameObject);
                gameManager.foodPieces.Remove(food);
                matches.Clear();
            }
        }
        else
        {
            print("NO MATCH");
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }



}
