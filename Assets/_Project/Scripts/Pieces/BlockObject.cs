using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

public class BlockObject : MonoBehaviour
{
    public BlockColor _blockColor;
    public BlockType _blockType;

    public Sprite myFrontSprite;

    protected Block myBlock;

    protected Animator animator;

    public List<BlockObject> neighbors = new();
    public List<Food> foods = new();

    public List<Food> processed = new();

    public LayerMask playerlayer;

    private string id;

    private float radius = 0.6f;

    public GameObject eatEffect;

    float fall;

    protected GameManager gameManager;

    public RaycastHit2D hit2D;

    public string ID
    {
        get => id;
        set
        {
            id = value;
            gameObject.name = _blockType.ToString() + _blockColor.ToString() + id;
        }
    }

    public void Initialize(Block block)
    {
        myBlock = block;
        _blockColor = myBlock.blockColor;
        _blockType = myBlock.blockType;
        myFrontSprite = myBlock.frontSprite;
        playerlayer = myBlock.layer;

        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = myBlock.blockAnimation;
    }

    private void Initialization()
    {
        gameManager = GameManager.GetInstance();
        
    }

    private void Start()
    {
        Initialization();
    }

    private void Update()
    {
        GridFitting();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FillNeighbors();
        SearchFood();
        FindAllFoodRecursively();
        
    }

    private void FillNeighbors()
    {
        Collider2D[] hitInfo;
        List<BlockObject> temporary = new();
        hitInfo = Physics2D.OverlapCircleAll(transform.position, radius, playerlayer);

        foreach (Collider2D c in hitInfo)
        {
            if(c.GetComponent<BlockObject>().ID != ID)
            {
                temporary.Add(c.GetComponent<BlockObject>());
            }
        }

        neighbors = temporary.Distinct().ToList();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);

    }


    private void SearchFood()
    {
        List<Food> temporary = new();
        
        for (int i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i]._blockColor == _blockColor && neighbors[i]._blockType == BlockType.Food)
            {
                temporary.Add(neighbors[i].GetComponent<Food>());
                // var additionalFood = neighbors[i].DetectFood();
            }
        }
        foods = temporary.Distinct().ToList();
    }
    
    protected virtual void FindAllFoodRecursively()
    {
        //
    }

    private void GridFitting()
    {
        hit2D = Physics2D.Raycast(transform.position, Vector2.down, 1);
        Debug.DrawRay(transform.position * 1, Vector2.down * 1, Color.red);

        if (!hit2D.collider && transform.parent == null)
        {
            transform.position += Vector3.down;
            print("to aqui");
        }
        else
        {
            return;

        }
    }

    public void AutomaticFall()
    {
        if (Time.time - fall >= 1)
        {
            transform.position += Vector3.down;
            fall = Time.time;
            FallAlgorithm();
        }
    }

    public void FallAlgorithm()
    {
        if (ValidPosition())
        {
            gameManager.UpdateGrid(GetComponentInParent<PiecesController>());
        }
        else
        {
            transform.position += Vector3.up;
        }
    }

    private bool ValidPosition()
    {
        Vector2 posBlock = gameManager.RoundValue(transform.position);

        if (!gameManager.InsideGrid(posBlock))
        {
            return false;
        }


        if (gameManager.PosTransformGrid(posBlock) != null && gameManager.PosTransformGrid(posBlock) != transform)
        {
            return false;
        }

        return true;
    }

}
