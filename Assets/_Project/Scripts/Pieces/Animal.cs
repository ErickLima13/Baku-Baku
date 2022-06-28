using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Animal : BlockObject
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    private void Update()
    {
        //gameManager.RoundValue(transform.position);
        //Mathf.RoundToInt(GetComponent<Rigidbody2D>().rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        FillNeighbors();

        //if (collision.gameObject.TryGetComponent(out Food food) && food._blockColor == _blockColor)
        //{
        //    gameManager.UpdateScore(1);
            
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FillNeighbors();
    }




}
