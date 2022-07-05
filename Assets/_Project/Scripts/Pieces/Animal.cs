using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Animal : BlockObject
{
    private GameManager gameManager;

    

    void Start()
    {
        gameManager = GameManager.GetInstance();
    }

  
}
