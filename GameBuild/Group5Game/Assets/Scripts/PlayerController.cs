using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    //Take-over mechanic
    int takeoverTimer;
    public GameObject controlObject;

    //Input control
    public KeyCode k_moveUp;
    public KeyCode k_moveRight;
    public KeyCode k_moveDown;
    public KeyCode k_moveLeft;

    public KeyCode k_swap;

    //player variables
    float movmentSpeed = 2.5f;

    Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        controlObject = this.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        MovementController(controlObject);
	}

    void ChangeControlObject(GameObject obj)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        rb.velocity = new Vector2(0, 0);
        controlObject = obj;
        rb = obj.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        if (obj != this.gameObject)
        {
            StartCoroutine(controlTimer(5));
        }
    }

    void MovementController(GameObject obj)
    {

        // Up - Left
        if (Input.GetKey(k_moveLeft) && Input.GetKey(k_moveUp))
        {
            rb.velocity = new Vector2(-movmentSpeed, movmentSpeed); // * Time.deltaTime;
        }
        //Up
        else if (Input.GetKey(k_moveUp) && !Input.GetKey(k_moveRight) && !Input.GetKey(k_moveLeft)) //if up and not right or left
        {
            rb.velocity = new Vector2(0, movmentSpeed);
        }
        //Up-Right
        else if (Input.GetKey(k_moveUp) && Input.GetKey(k_moveRight))
        {
            rb.velocity = new Vector2(movmentSpeed, movmentSpeed);
        }
        //Right
        else if (Input.GetKey(k_moveRight) && !Input.GetKey(k_moveUp) && !Input.GetKey(k_moveDown)) //if right and not up or down
        {
            rb.velocity = new Vector2(movmentSpeed, 0);
        }
        //Down-Right
        else if (Input.GetKey(k_moveDown) && Input.GetKey(k_moveRight))
        {
            rb.velocity = new Vector2(movmentSpeed, -movmentSpeed);
        }
        //Down
        else if (Input.GetKey(k_moveDown) && !Input.GetKey(k_moveRight) && !Input.GetKey(k_moveLeft)) //if down and not right or left
        {
            rb.velocity = new Vector2(0, -movmentSpeed);
        }
        //Down-Left
        else if (Input.GetKey(k_moveDown) && Input.GetKey(k_moveLeft))
        {
            rb.velocity = new Vector2(-movmentSpeed, -movmentSpeed);
        }
        //Left
        else if (Input.GetKey(k_moveLeft) && !Input.GetKey(k_moveUp) && !Input.GetKey(k_moveDown)) //if left and not up or down
        {
            rb.velocity = new Vector2(-movmentSpeed, 0);
        }
        else if (!Input.GetKey(k_moveUp) && !Input.GetKey(k_moveRight) && !Input.GetKey(k_moveDown) && !Input.GetKey(k_moveLeft))
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void OnTriggerStay2D(Collider2D colObj)
    {
        if (Input.GetKey(k_swap))
        {
            ChangeControlObject(colObj.gameObject);
        }
    }

    IEnumerator controlTimer (int controlTime)
    {
        yield return new WaitForSeconds(controlTime);
        ChangeControlObject(this.gameObject);
    }

}//end of class