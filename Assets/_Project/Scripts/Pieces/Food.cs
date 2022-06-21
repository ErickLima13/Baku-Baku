using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : MonoBehaviour
{
    public BlockColor _foodBlockColor;

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
        if (collision.gameObject.TryGetComponent(out Animal animal) && animal._animalBlockColor == _foodBlockColor)
        {
            //print(animal.animalColors);
            //print("PEGOU");

            //Destroy(collision.gameObject, 0.2f);
            //Destroy(this.gameObject, 0.5f);

            


        }
    }

    
}
