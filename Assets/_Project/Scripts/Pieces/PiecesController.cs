using UnityEngine;

public class PiecesController : MonoBehaviour
{

    [SerializeField] private int Id;

    [SerializeField] private float fall;
    [SerializeField] private float speed;
    [SerializeField] private float timer;

    private GameManager gameManager;

    private Spawner spawner;

    private void Initialization()
    {
        gameManager = FindObjectOfType<GameManager>();
        spawner = FindObjectOfType<Spawner>();
        timer = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
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
            }
        }

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
