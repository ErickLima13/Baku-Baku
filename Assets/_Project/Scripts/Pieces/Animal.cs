using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Animal : MonoBehaviour
{
    public BlockColor _animalBlockColor;
    private GameManager gameManager;

    private void Initialization()
    {
        gameManager = GameManager.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Food food) && food._foodBlockColor == _animalBlockColor)
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
                if (_animalBlockColor == gameManager.foodPieces[i].GetComponent<Food>()._foodBlockColor)
                {
                    hItems.Add(gameManager.foodPieces[i].GetComponent<Food>());
                }
            }
        }

        return hItems;
    }

}
