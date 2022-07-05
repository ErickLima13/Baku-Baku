using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PiecesController : MonoBehaviour
{
    [SerializeField] private float fall;
    [SerializeField] private float speed;
    [SerializeField] private float timer;

    private GameManager gameManager;
    private Spawner spawner;

    private List<BlockObject> blocks = new List<BlockObject>();

    [Header("Touch")]
    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    public void Initialize(Block animalBlock, Block foodBlock, int value)
    {
        gameManager = GameManager.GetInstance();
        spawner = Spawner.GetInstance();
        timer = speed;

        var randomChoice = Random.Range(0, 2);
        if (randomChoice == 0)
        {
            transform.GetChild(0).AddComponent<Food>();
            transform.GetChild(1).AddComponent<Animal>();
            blocks.Add(transform.GetChild(0).GetComponent<Food>());
            blocks.Add(transform.GetChild(1).GetComponent<Animal>());
            blocks[0].Initialize(foodBlock);
            blocks[1].Initialize(animalBlock);

            blocks[0].ID = "F" + value.ToString();
            blocks[1].ID = "A" + value.ToString();


        }
        else
        {
            transform.GetChild(0).AddComponent<Animal>();
            transform.GetChild(1).AddComponent<Food>();
            blocks.Add(transform.GetChild(0).GetComponent<Animal>());
            blocks.Add(transform.GetChild(1).GetComponent<Food>());
            blocks[0].Initialize(animalBlock);
            blocks[1].Initialize(foodBlock);

            blocks[0].ID = "A" + value.ToString();
            blocks[1].ID = "F" + value.ToString();
        }

        for(int i = 0; i < blocks.Count; i++)
        {
            blocks[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Update()
    {
        if (gameManager.isGameOver)
        {
            return;
        }

        if (!gameManager.isPaused)
        {
#if UNITY_EDITOR
            Move();
#elif UNITY_ANDROID
            Swipe();
#endif
        }

    }

    public void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
            timer = speed;
        }

        AutomaticFall();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

            if (!stopTouch)
            {

                if (Distance.x < -swipeRange)
                {
                    //outputText.text = "Left";
                    stopTouch = true;

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
                else if (Distance.x > swipeRange)
                {
                    //outputText.text = "Right";
                    stopTouch = true;

                    timer += Time.deltaTime;

                    if (timer > speed)
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
                else if (Distance.y > swipeRange)
                {
                    //outputText.text = "Up";
                    stopTouch = true;
                }
                else if (Distance.y < -swipeRange)
                {
                    //outputText.text = "Down";
                    stopTouch = true;

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

                        AnimationFall();

                        if (!gameManager.isGameOver)
                        {
                            spawner.SpawnPieces();
                        }

                        if (gameManager.AboveGrid(this))
                        {
                            gameManager.GameOver();
                        }
                    }

                }

            }

        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;

            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {
                //outputText.text = "Tap";

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

        }


    }
    private void Move()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            timer = speed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            timer += Time.deltaTime;

            if (timer > speed)
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

            PieceValidPosition();
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
    }

    private void AnimationFall()
    {
        if (blocks[0] != null)
        {
            blocks[0].GetComponent<BoxCollider2D>().enabled = true;
            blocks[0].GetComponent<SpriteRenderer>().sprite = blocks[0].myFrontSprite;
            blocks[0].GetComponent<Animator>().enabled = false;
        }

        if (blocks[1] != null)
        {
            blocks[1].GetComponent<BoxCollider2D>().enabled = true;
            blocks[1].GetComponent<SpriteRenderer>().sprite = blocks[1].myFrontSprite;
            blocks[1].GetComponent<Animator>().enabled = false;
        }
    }

    private void AutomaticFall()
    {
        if (Time.time - fall >= (1 / gameManager.difficulty) && !Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down;
            fall = Time.time;
            PieceValidPosition();
        }
    }

    private void PieceValidPosition()
    {
        if (ValidPosition())
        {
            gameManager.UpdateGrid(this);
        }
        else
        {
            transform.position += Vector3.up;
            enabled = false;
            AnimationFall();

            if (!gameManager.isGameOver)
            {
                spawner.SpawnPieces();
            }

            if (gameManager.AboveGrid(this))
            {
                gameManager.GameOver();
            }
        }

        gameManager.pieces = GameObject.FindGameObjectsWithTag("Player");
    }

    private bool ValidPosition()
    {
        List<Transform> transformsList = new();
        
        foreach (Transform child in transform)
        {
            transformsList.Add(child);

            Vector2 posBlock = gameManager.RoundValue(child.position);

            if (!gameManager.InsideGrid(posBlock))
            {
                return false;
            }

            if (gameManager.PosTransformGrid(posBlock) != null && gameManager.PosTransformGrid(posBlock).parent != transform)
            {
                child.parent = null;
                transformsList.Remove(child);
            }
        }

        if (transformsList.Any() == false)
        {
            return false;
        }

        return true;
    }

}
