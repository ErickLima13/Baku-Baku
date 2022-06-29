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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FillNeighbors();
    }


}
