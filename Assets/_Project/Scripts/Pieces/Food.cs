using UnityEngine;


public class Food : BlockObject
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FillNeighbors();
        SearchFood();

        if (collision.gameObject.TryGetComponent(out Animal animal) && animal._blockColor == _blockColor)
        {

           
            print(animal.name);



            //animal.neighbors.Add(this);
            //animal.SearchFood();

            animal.foods.Add(this);
            

        }
    }

}
