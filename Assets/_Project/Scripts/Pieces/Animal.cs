using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Animal : MonoBehaviour
{
    public Colors animalColors;
    private GameManager gameManager;

    private void Initialization()
    {
        gameManager = FindObjectOfType<GameManager>();
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
        if (collision.gameObject.TryGetComponent(out Food food) && food.foodColors == animalColors)
        {
            //print(food.foodColors);
            //print("PEGOU 2");

            //Destroy(collision.gameObject, 0.2f);
            //Destroy(this.gameObject, 0.5f);

            print(food);
            gameManager.CheckGrid(food.gameObject);
        }

        

    }

}
