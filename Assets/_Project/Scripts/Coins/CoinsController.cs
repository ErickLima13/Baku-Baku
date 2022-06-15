using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController : MonoBehaviour
{

    [Range(0, 5)][SerializeField] private float speedFall;


    private void Initialization()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < 5)
        {

        }
        
    }

    private void Fall()
    {
        transform.Translate(speedFall * Time.deltaTime * Vector3.down);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //speedFall = 0;
        }
    }

   
}
