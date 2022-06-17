using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> Pieces = new List<GameObject>();

    [SerializeField] private List<Quaternion> rotations = new List<Quaternion>();

    [SerializeField] private int countPieces;

    [SerializeField] private float timeSpawn;

    private void Initialization()
    {
        SpawnPieces();
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

    public void SpawnPieces()
    {
        //timeSpawn -= Time.deltaTime;

        //if (timeSpawn <= 0)
        //{
        //    countPieces = 0;
        //}

        //if (countPieces == 0 && timeSpawn <= 0)
        //{
            
            
        //}

        int index = Random.Range(0, Pieces.Count);
        Instantiate(Pieces[index], RandomPos(), RandomRot());

        //countPieces++;
        //timeSpawn = 5;


    }

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(2, 6), 18, 0);
    }

    private Quaternion RandomRot()
    {
        int index = Random.Range(0, rotations.Count);
        return rotations[index];
    }
   
}

