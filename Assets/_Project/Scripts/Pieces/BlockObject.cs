using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockObject : MonoBehaviour
{
    public BlockColor _blockColor;
    public BlockType _blockType;

    public Sprite myFrontSprite;

    private Block myBlock;

    protected Animator animator;

    public List<BlockObject> neighbors = new();
    public List<BlockObject> foods = new();

    public LayerMask playerlayer;

    private string id;

    private float radius = 0.6f;

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        FillNeighbors();
        SearchFood();
    }

    private void DetectNeighbors()
    {
        if(_blockType == BlockType.Food)
        {

        }
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
        List<BlockObject> temporary = new();

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i]._blockColor == _blockColor && neighbors[i]._blockType == BlockType.Food)
            {
                temporary.Add(neighbors[i]);
            }
        }

        foods = temporary.Distinct().ToList();
    }
}
