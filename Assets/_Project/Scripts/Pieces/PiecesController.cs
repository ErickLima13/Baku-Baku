using UnityEngine;

public class PiecesController : MonoBehaviour
{

    [SerializeField] private float speedFall;
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

            if (PosicaoValida())
            {

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

            if (PosicaoValida())
            {

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

            if (PosicaoValida())
            {

            }
            else
            {
                transform.position += Vector3.up;
                enabled = false;
                spawner.SpawnPieces();
            }
        }

        if (Time.time - speedFall >= 1 && !Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down;

            if (PosicaoValida())
            {

            }
            else
            {
                transform.position += Vector3.up;
            }

            speedFall = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 90);

            if (PosicaoValida())
            {

            }
            else
            {
                transform.Rotate(0, 0, -90);
            }
        }

        
       

        


        //transform.Translate(speedFall * Time.deltaTime * Vector3.down);


    }

    private bool PosicaoValida()
    {
        foreach(Transform child in transform)
        {
            Vector2 posBloco = gameManager.Arredonda(child.position);

            if (!gameManager.DentroGrade(posBloco))
            {
                return false;
            }
        }

        return true;
    }

   
}
