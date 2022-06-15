using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> coins = new List<GameObject>();

    [Range(0, 5)][SerializeField] private float speedFall;

    [SerializeField] private int countCoins;

    [SerializeField] private float timeSpawn;

    private void Initialization()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        // Move a moeda para baixo
        //coins[0].transform.Translate(Vector3.down * speedFall * Time.deltaTime);



        timeSpawn -= Time.deltaTime;

        if (timeSpawn <= 0)
        {
            countCoins = 0;
        }

       

        if (countCoins == 0 && timeSpawn <= 0)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(coins[(Random.Range(0, coins.Count))], RandomPos(), Quaternion.identity);
                countCoins++;
                
            }

            timeSpawn = 5;

            
        }

     


    }

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-2, 2) , 5 , 0);
    }


   
}

