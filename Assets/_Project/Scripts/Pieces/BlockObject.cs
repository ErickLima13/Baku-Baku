using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BlockObject : MonoBehaviour
{
    [SerializeField] private BlockColor _blockColor;
    [SerializeField] private BlockType _blockType;
    
    private Block myBlock;
    
    protected Animator animator;

    public void Initialize(Block block)
    {
        myBlock = block;
        _blockColor = myBlock.blockColor;
        _blockType = myBlock.blockType;
        
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = myBlock.blockAnimation;;
    }
}
