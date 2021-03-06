using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PiecesController : MonoBehaviour
{
    [SerializeField] private float fall;
    [SerializeField] private float speed;
    [SerializeField] private float timer;

    private GameManager gameManager;
    private Spawner spawner;

    private List<BlockObject> blocks = new List<BlockObject>();

    [Header("Touch")] private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    public List<BlockObject> Blocks => blocks;

    [SerializeField] private GameObject eatEffect;

    #region Components Blocks

    public BoxCollider2D boxColliderBlock;
    public BoxCollider2D boxColliderBlock2;

    public RaycastDetect raycast;
    public RaycastDetect raycast2;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteRenderer2;

    public Animator animator;
    public Animator animator2;

    #endregion


    public void Initialize(Block animalBlock, Block foodBlock, int value)
    {
        var randomChoice = Random.Range(0, 2);
        if (randomChoice == 0)
        {
            transform.GetChild(0).AddComponent<Food>();
            transform.GetChild(1).AddComponent<Animal>();
            blocks.Add(transform.GetChild(0).GetComponent<Food>());
            blocks.Add(transform.GetChild(1).GetComponent<Animal>());
            blocks[0].eatEffect = eatEffect;
            blocks[1].eatEffect = eatEffect;
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
            blocks[0].eatEffect = eatEffect;
            blocks[1].eatEffect = eatEffect;
            blocks[0].Initialize(animalBlock);
            blocks[1].Initialize(foodBlock);

            blocks[0].ID = "A" + value.ToString();
            blocks[1].ID = "F" + value.ToString();
        }

        foreach (Transform child in transform)
        {
            child.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z * -1);
        }
    }

    private void Initialization()
    {
        gameManager = GameManager.GetInstance();
        spawner = Spawner.GetInstance();
        timer = speed;
    }

    private void Start()
    {
        Initialization();
    }

    private void Update()
    {
        if (!gameManager.isPaused)
        {
            //#if UNITY_EDITOR
            Move();
            //#elif UNITY_ANDROID
            Swipe();
            //#endif
            AutomaticFall();
            SelfDestruct();
        }
    }

    private void SelfDestruct()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject, 1f);

            //gameObject.SetActive(false);
        }
    }

    public void Swipe()
    {
        if (transform.childCount <= 1)
        {
            return;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
            timer = speed;
        }

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

                    FallAlgorithm();
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
                foreach (Transform child in transform)
                {
                    child.Rotate(0, 0, -90);
                }

                if (ValidPosition())
                {
                    gameManager.UpdateGrid(this);
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                    foreach (Transform child in transform)
                    {
                        child.Rotate(0, 0, 90);
                    }
                }
            }
        }
    }

    private void Move()
    {
        if (transform.childCount <= 1)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.DownArrow))
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

            FallAlgorithm();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 90);
            foreach (Transform child in transform)
            {
                child.Rotate(0, 0, -90);
            }

            if (ValidPosition())
            {
                gameManager.UpdateGrid(this);
            }
            else
            {
                transform.Rotate(0, 0, -90);
                foreach (Transform child in transform)
                {
                    child.Rotate(0, 0, 90);
                }
            }
        }
    }

    private void AnimationFall()
    {
        if (blocks[0] != null)
        {
            boxColliderBlock.enabled = true;
            raycast.enabled = true;
            spriteRenderer.sprite = blocks[0].myFrontSprite;
            animator.enabled = false;
        }

        if (blocks[1] != null)
        {
            boxColliderBlock2.enabled = true;
            raycast2.enabled = true;
            spriteRenderer2.sprite = blocks[1].myFrontSprite;
            animator2.enabled = false;
        }
    }

    private void AutomaticFall()
    {
        var difficulty = gameManager.difficulty;
        if (transform.childCount <= 1)
        {
            difficulty = gameManager.difficulty * 8;
        }

        if (Time.time - fall >= (1 / difficulty) && !(Input.GetKey(KeyCode.DownArrow) && transform.childCount > 1))
        {
            transform.position += Vector3.down;
            fall = Time.time;
            FallAlgorithm();
        }
    }

    public void FallAlgorithm()
    {
        List<Transform> badChildren = GetInvalidChildrenPositions();
        if (badChildren.Any() == false)
        {
            gameManager.UpdateGrid(this);
        }
        else
        {
            transform.position += Vector3.up;
            foreach (var child in badChildren)
            {
                child.parent = null;
            }

            if (transform.childCount == 0)
            {
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

        gameManager.pieces = GameObject.FindGameObjectsWithTag("Player");
    }

    private bool ValidPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 posBlock = gameManager.RoundValue(child.position);

            if (!gameManager.InsideGrid(posBlock))
            {
                return false;
            }

            //Se tem algo na posi????o que quero E o pai que est?? na posi????o ?? diferente do meu pai
            if (gameManager.PosTransformGrid(posBlock) != null &&
                gameManager.PosTransformGrid(posBlock).parent != transform)
            {
                return false;
            }
        }

        return true;
    }

    public List<Transform> GetInvalidChildrenPositions()
    {
        List<Transform> badChildren = new List<Transform>();
        foreach (Transform child in transform)
        {
            Vector2 posBlock = gameManager.RoundValue(child.position);
            if (!gameManager.InsideGrid(posBlock))
            {
                badChildren.Add(child);
            }
            //Se tem algo na posi????o que quero E o pai que est?? na posi????o ?? diferente do meu pai
            else if (gameManager.PosTransformGrid(posBlock) != null &&
                     gameManager.PosTransformGrid(posBlock).parent != transform)
            {
                badChildren.Add(child);
            }
        }

        return badChildren;
    }
}