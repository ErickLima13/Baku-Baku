using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BlockObject : MonoBehaviour
{
    public BlockColor _blockColor;
    public BlockType _blockType;
    
    private Block myBlock;
    
    protected Animator animator;

    public void Initialize(Block block)
    {
        myBlock = block;
        _blockColor = myBlock.blockColor;
        _blockType = myBlock.blockType;
        
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = myBlock.blockAnimation;
    }
}
