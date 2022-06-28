using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Block")]
public class Block : ScriptableObject
{
    public BlockType blockType;
    public BlockColor blockColor;
    public AnimatorOverrideController blockAnimation;
    public Sprite frontSprite;
    public LayerMask layer;
}

public enum BlockColor
{
    Purple,
    Yellow,
    Green,
    BabyBlue,
    Red
}

public enum BlockType
{
    Animal,
    Food
}
