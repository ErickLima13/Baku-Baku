using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] private List<GameObject> Pieces = new();

    [SerializeField] private List<Vector3> rotations = new();

    [SerializeField] private int countPieces;

    [SerializeField] private float timeSpawn;

    [SerializeField] private PiecesController _piecesController;
    [SerializeField] private List<Block> animalBlocks = new List<Block>();
    [SerializeField] private List<Block> foodBlocks = new List<Block>();

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
        var rotation = Quaternion.Euler(RandomRot());
        var newPieceController = Instantiate(_piecesController, RandomPos(), rotation);
        StartCoroutine(TryInitializePieceController(newPieceController));
        //countPieces++;
        //timeSpawn = 5;
    }

    /// <summary>
    /// While loop que tenta pegar peças que não sejam da mesma cor.
    /// </summary>
    /// <param name="newPieceController"></param>
    /// <returns></returns>
    private IEnumerator TryInitializePieceController(PiecesController newPieceController)
    {
        bool valid = false;
        Block animal = animalBlocks[0];
        Block food = foodBlocks[0];
        while (!valid)
        {
            animal = animalBlocks[Random.Range(0, animalBlocks.Count)];
            food = foodBlocks[Random.Range(0, foodBlocks.Count)];
            if (animal.blockColor != food.blockColor)
            {
                valid = true;
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
        newPieceController.Initialize(animal, food);
    }

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(2, 5), 18, 0);
    }

    private Vector3 RandomRot()
    {
        int index = Random.Range(0, rotations.Count);
        return rotations[index];
    }

}

