using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockObject : MonoBehaviour
{
    public BlockColor _blockColor;
    public BlockType _blockType;

    public Sprite myFrontSprite;

    private Block myBlock;

    protected Animator animator;

    public List<BlockObject> neighbors = new();

    public LayerMask playerlayer;

    public string ID;

    private float radius = 0.6f;

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

    public void FillNeighbors()
    {
        Collider2D[] hitInfo;
        List<BlockObject> temporary = new();
        hitInfo = Physics2D.OverlapCircleAll(transform.position, radius, playerlayer);

        foreach (Collider2D c in hitInfo)
        {
            temporary.Add(c.GetComponent<BlockObject>());
        }

        neighbors = temporary.Distinct().ToList();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
