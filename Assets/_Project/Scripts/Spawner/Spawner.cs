using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> Pieces = new List<GameObject>();

    [Range(0, 5)][SerializeField] private float speedFall;

    [SerializeField] private int countPieces;

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
        SpawnPieces();
    }

    private void SpawnPieces()
    {
        

       

        timeSpawn -= Time.deltaTime;

        if (timeSpawn <= 0)
        {
            countPieces = 0;
        }

        if (countPieces == 0 && timeSpawn <= 0)
        {
            for (int i = 0; i < 2; i++)
            {
                int index = Random.Range(0, Pieces.Count);
                Instantiate(Pieces[index], RandomPos(), Quaternion.identity);
                countPieces++;
                
            }

            timeSpawn = 5; 
            
        }

     


    }

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-3.8f, 3.8f), 10, 0);
    }


   
}

