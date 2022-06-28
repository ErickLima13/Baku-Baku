using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : BlockObject
{

    private GameManager gameManager;

    private void Update()
    {

        //gameManager.RoundValue(transform.position);
        //Mathf.RoundToInt(GetComponent<Rigidbody2D>().rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FillNeighbors();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FillNeighbors();
    }
}
