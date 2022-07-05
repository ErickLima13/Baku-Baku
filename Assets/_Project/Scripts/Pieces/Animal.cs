using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Animal : BlockObject
{
    private GameManager gameManager;

    

    void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    protected override void FindAllFoodRecursively()
    {
        base.FindAllFoodRecursively();
        if (foods.Any() == false) return;
        List<Food> toSearch = new List<Food>(){foods[0]};
        processed = new List<Food>();
        while (toSearch.Any())
        {
            Food current = toSearch[0];
            processed.Add(current);
            toSearch.Remove(current);

            foreach (var food in current.foods)
            {
                bool inSearch = processed.Contains(food);

                if (!inSearch)
                {
                    toSearch.Add(food);
                }
            }
        }

        if (processed.Any())
        {
            StartCoroutine(EatAnimation());
        }
    }

    private IEnumerator EatAnimation()
    {
        throw new NotImplementedException();
    }
}
