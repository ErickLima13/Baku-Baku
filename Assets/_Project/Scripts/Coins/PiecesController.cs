using UnityEngine;

public class PiecesController : MonoBehaviour
{

    [SerializeField] private float speedFall;



    private void Initialization()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {

        Fall();
    }

    private void Fall()
    {
        transform.Translate(speedFall * Time.deltaTime * Vector3.down);

        //if (Time.time - speedFall >= 1)
        //{
        //    transform.position += new Vector3(0, -1, 0);

        //    speedFall = Time.time;
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GetComponent<Rigidbody2D>().gravityScale = 5;
        enabled = false;


        switch (collision.gameObject.tag)
        {
            case "Ground":
                enabled = false;
                break;
        }
    }
}
