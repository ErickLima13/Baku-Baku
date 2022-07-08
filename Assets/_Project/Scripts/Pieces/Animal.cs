using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Animal : BlockObject
{
    private GameObject cloneEatEffect;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererClone;

    private Animator animatorClone;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        cloneEatEffect = Instantiate(eatEffect, transform);
        cloneEatEffect.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRendererClone = cloneEatEffect.GetComponent<SpriteRenderer>();
        animatorClone = cloneEatEffect.GetComponent<Animator>();
    }

    protected override void FindAllFoodRecursively()
    {
        base.FindAllFoodRecursively();
        if (foods.Any() == false) return;
        List<Food> toSearch = new List<Food>() { foods[0] };
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
        yield return new WaitForSeconds(0.1f);

        spriteRenderer.enabled = false;
        cloneEatEffect.SetActive(true);
        animatorClone.runtimeAnimatorController = myBlock.overrideController;

        foreach (Food f in processed)
        {
            if (f != null)
            {
                if(f.transform.position.x >= 3)
                {
                    spriteRendererClone.flipX = true;
                }

                transform.position = f.transform.position;
                //Destroy(f.gameObject, 0.1f);
                f.gameObject.SetActive(false);
                gameManager.pieces.Remove(f.gameObject);
                yield return new WaitForSeconds(0.2f);
            }
        }

        gameManager.UpdateScore(processed.Count);
        //processed.Clear();
        Destroy(gameObject, 0.7f);
        gameManager.pieces.Remove(gameObject);
        //yield return new WaitForSeconds(0.7f);
        //gameObject.SetActive(false);
        StopAllCoroutines();
    }
}
