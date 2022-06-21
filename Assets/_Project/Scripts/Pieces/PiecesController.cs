using System.Collections.Generic;
using UnityEngine;

public class PiecesController : MonoBehaviour
{
    [SerializeField] private float fall;
    [SerializeField] private float speed;
    [SerializeField] private float timer;

    private GameManager gameManager;

    private Spawner spawner;

    private List<BlockObject> blocks = new List<BlockObject>();

    public void Initialize(Block animalBlock, Block foodBlock)
    {
        gameManager = FindObjectOfType<GameManager>();
        spawner = FindObjectOfType<Spawner>();
        timer = speed;
        
        blocks.Add(transform.GetChild(0).GetComponent<BlockObject>());
        blocks.Add(transform.GetChild(1).GetComponent<BlockObject>());
        var randomChoice = Random.Range(0, 2);
        if (randomChoice == 0)
        {
            blocks[0].Initialize(foodBlock);
            blocks[1].Initialize(animalBlock);
        }
        else
        {
            blocks[0].Initialize(animalBlock);
            blocks[1].Initialize(foodBlock);
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            timer = speed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            timer += Time.deltaTime;

            if(timer > speed)
            {
                transform.position += Vector3.right;
                timer = 0;
            }

            if (ValidPosition())
            {
                gameManager.UpdateGrid(this);
               
            }
            else
            {
                transform.position += Vector3.left;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {

            timer += Time.deltaTime;

            if (timer > speed)
            {
                transform.position += Vector3.left;
                timer = 0;
            }

            if (ValidPosition())
            {
                gameManager.UpdateGrid(this);
                
            }
            else
            {
                transform.position += Vector3.right;
            }

        }

        if (Input.GetKey(KeyCode.DownArrow)) 
        {
            timer += Time.deltaTime;

            if (timer > speed)
            {
                transform.position += Vector3.down;
                timer = 0;
            }

            if (ValidPosition())
            {
                gameManager.UpdateGrid(this);
                
            }
            else
            {
                transform.position += Vector3.up;
                enabled = false;
                spawner.SpawnPieces();

                if (GetComponentInChildren<Food>())
                {
                    gameManager.GetFood(gameObject.GetComponentInChildren<Food>());
                }
                
            }
        }

        AutomaticFall();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 90);

            if (ValidPosition())
            {
                gameManager.UpdateGrid(this);
            }
            else
            {
                transform.Rotate(0, 0, -90);
            }
        }

        
       

        


        //transform.Translate(speedFall * Time.deltaTime * Vector3.down);


    }

    private void AutomaticFall()
    {
        if (Time.time - fall >= 1 && !Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down;

            if (ValidPosition())
            {
                gameManager.UpdateGrid(this);
            }
            else
            {
                transform.position += Vector3.up;
                enabled = false;
                spawner.SpawnPieces();
            }

            fall = Time.time;
        }
    }

    private bool ValidPosition()
    {
        foreach(Transform child in transform)
        {
            Vector2 posBlock = gameManager.RoundValue(child.position);

            if (!gameManager.InsideGrid(posBlock))
            {
                return false;
            }

            if (gameManager.PosTransformGrid(posBlock) != null && gameManager.PosTransformGrid(posBlock).parent != transform)
            {
                return false;
            }
        }

        return true;
    }
}
