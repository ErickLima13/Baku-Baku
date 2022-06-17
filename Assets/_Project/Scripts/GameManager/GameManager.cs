using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int altura = 18;
    public static int largura = 6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool DentroGrade(Vector2 posicao)
    {
        return (int)posicao.x > 0 && (int)posicao.x <= largura && (int)posicao.y > 0;
    }

    public Vector2 Arredonda(Vector2 nA)
    {
        return new Vector2(Mathf.Round(nA.x), Mathf.Round(nA.y));
    }
   
}
