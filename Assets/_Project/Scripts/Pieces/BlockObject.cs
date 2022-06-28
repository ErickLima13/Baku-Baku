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

    public string ID;

    public float radius = 0.3f;

    public void Initialize(Block block)
    {
        myBlock = block;
        _blockColor = myBlock.blockColor;
        _blockType = myBlock.blockType;
        myFrontSprite = myBlock.frontSprite;
        playerlayer = myBlock.layer;

        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = myBlock.blockAnimation;
        neighbors.Add(this);
    }

    public void FillNeighbors()
    {
        //neighbors.Add(Physics2D.OverlapPoint(Vector2.up, playerlayer).GetComponent<BlockObject>());
        //neighbors.Add(Physics2D.OverlapPoint(Vector2.down, playerlayer).GetComponent<BlockObject>());
        //neighbors.Add(Physics2D.OverlapPoint(Vector2.left, playerlayer).GetComponent<BlockObject>());
        //neighbors.Add(Physics2D.OverlapPoint(Vector2.right, playerlayer).GetComponent<BlockObject>());

        Collider2D col = Physics2D.OverlapCircle(transform.position + Vector3.up, radius, playerlayer);
        Collider2D col1 = Physics2D.OverlapCircle(Vector3.down + transform.position, radius, playerlayer);
        Collider2D col2 = Physics2D.OverlapCircle(Vector3.left + transform.position, radius, playerlayer);
        Collider2D col3 = Physics2D.OverlapCircle(Vector3.right + transform.position, radius, playerlayer);

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (col.GetComponent<BlockObject>().ID != neighbors[i].ID)
            {
                neighbors.Add(col.GetComponent<BlockObject>());
            }

            if (col1.GetComponent<BlockObject>().ID != neighbors[i].ID)
            {
                neighbors.Add(col.GetComponent<BlockObject>());
            }

            if (col2.GetComponent<BlockObject>().ID != neighbors[i].ID)
            {
                neighbors.Add(col.GetComponent<BlockObject>());
            }

            if (col3.GetComponent<BlockObject>().ID != neighbors[i].ID)
            {
                neighbors.Add(col.GetComponent<BlockObject>());
            }
        }



        //neighbors.Add(Physics2D.OverlapCircle(transform.position, 1f, playerlayer).GetComponent<BlockObject>());


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Vector3.up + transform.position, radius);
        Gizmos.DrawWireSphere(Vector3.down + transform.position, radius);
        Gizmos.DrawWireSphere(Vector3.left + transform.position, radius);
        Gizmos.DrawWireSphere(Vector3.right + transform.position, radius);


    }
}
