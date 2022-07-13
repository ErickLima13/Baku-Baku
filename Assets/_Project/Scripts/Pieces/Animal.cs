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

    //private void LateUpdate()
    //{
    //    if (transform.parent == null)
    //    {
    //        FillNeighbors();
    //        SearchFood();

    //        if (foods.Count > 0)
    //        {
    //            FindAllFoodRecursively();
    //        }
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FillNeighbors();
        SearchFood();
        FindAllFoodRecursively();

        if (collision.gameObject.TryGetComponent(out Food food) && food._blockColor == _blockColor)
        {
            
        }
    }

    public  void FindAllFoodRecursively()
    {
        //base.FindAllFoodRecursively();
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

    public IEnumerator EatAnimation()
    {
        yield return new WaitForSeconds(0.2f);

        spriteRenderer.enabled = false;
        cloneEatEffect.SetActive(true);
        animatorClone.runtimeAnimatorController = myBlock.overrideController;

        foreach (Food f in processed)
        {
            if (f != null)
            {
                if (f.transform.position.x >= 3)
                {
                    spriteRendererClone.flipX = true;
                }

                f.neighbors.Remove(f);

                transform.position = f.transform.position;

                //yield return new WaitForSeconds(0.1f);

                Destroy(f.gameObject, 0.2f);

            }
        }



        //gameManager.UpdateScore(processed.Count);
        processed.Clear();
        Destroy(gameObject, 1f);
        //StopAllCoroutines();
    }


}
