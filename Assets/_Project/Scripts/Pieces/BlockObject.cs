using System.Collections.Generic;
using System.Linq;
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

    protected GameManager gameManager;

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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    FillNeighbors();
    //    SearchFood();
    //    //FindAllFoodRecursively();
    //}

    public void FillNeighbors()
    {
        Collider2D[] hitInfo;
        List<BlockObject> temporary = new();
        hitInfo = Physics2D.OverlapCircleAll(transform.position, radius, playerlayer);

        foreach (Collider2D c in hitInfo)
        {
            if (c.GetComponent<BlockObject>().ID != ID)
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

    public void SearchFood()
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

    //protected virtual void FindAllFoodRecursively()
    //{
    //    //
    //}

    private void OnDestroy()
    {
        gameManager.UpdateScore(1);
    }
}
