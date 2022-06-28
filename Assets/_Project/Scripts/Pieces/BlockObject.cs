using System.Collections.Generic;
using UnityEngine;

public class BlockObject : MonoBehaviour
{
    public BlockColor _blockColor;
    public BlockType _blockType;

    public Sprite myFrontSprite;

    private Block myBlock;

    protected Animator animator;

    public List<BlockObject> neighbors = new();

    public LayerMask playerlayer;

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
        neighbors.Add(Physics2D.OverlapPoint(Vector2.up, playerlayer).GetComponent<BlockObject>());
        neighbors.Add(Physics2D.OverlapPoint(Vector2.down, playerlayer).GetComponent<BlockObject>());
        neighbors.Add(Physics2D.OverlapPoint(Vector2.left, playerlayer).GetComponent<BlockObject>());
        neighbors.Add(Physics2D.OverlapPoint(Vector2.right, playerlayer).GetComponent<BlockObject>());
    }
}
