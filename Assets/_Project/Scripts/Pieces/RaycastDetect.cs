using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetect : MonoBehaviour
{
    private RaycastHit2D hit2D;

    private void FixedUpdate()
    {
        if (transform.parent == null)
        {
            GridFitting();
        }
    }

    private void GridFitting()
    {
        hit2D = Physics2D.Raycast(transform.position, Vector2.down, 1);
        //Debug.DrawRay(transform.position * 1, Vector2.down * 1, Color.red);

        if (!hit2D.collider)
        {
            transform.position += Vector3.down;
            //print("to aqui");
        }
        else
        {
            return;
        }
    }
}
