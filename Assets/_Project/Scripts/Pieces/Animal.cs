using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Animal : MonoBehaviour
{

    public Colors animalColors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Food food) && food.foodColors == animalColors)
        {
            print(food.foodColors);
            print("PEGOU 2");

            Destroy(collision.gameObject,0.2f);
            Destroy(this.gameObject, 0.5f);
        }

        print("COLID");
    }

}
