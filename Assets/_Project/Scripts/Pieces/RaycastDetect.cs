using UnityEngine;

public class RaycastDetect : MonoBehaviour
{
    private RaycastHit2D hit2D;
    public bool updatedGrid;

    private void FixedUpdate()
    {
        if (transform.parent == null)
        {
            GridFitting();
        }

        if(transform.position.y <= 0)
        {
            enabled = false;
        }
    }

    private void GridFitting()
    {
        hit2D = Physics2D.Raycast(transform.position, Vector2.down, 1);
        Debug.DrawRay(transform.position * 1, Vector2.down * 1, Color.red);

        if (!hit2D.collider)
        {
            updatedGrid = false;
            transform.position += Vector3.down;
        }
        else if (!updatedGrid)
        {
            updatedGrid = true;
            GameManager.GetInstance().UpdateGrid(transform);
        }
    }
}
