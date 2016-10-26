using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	//Global
	int level = 1;

	//Level1 Obstacles
	GameObject level1_door1;

	//Level1 Guards
	GameObject[] guards;
	GameObject level1_guard1;
	GameObject level1_guard2;

	//Game items
	public bool hasLevel1_key1 = false;

    //Take-over mechanic
    int takeoverTimer;
    public GameObject controlObject;

    //Input control
    public KeyCode k_moveUp;
    public KeyCode k_moveRight;
    public KeyCode k_moveDown;
    public KeyCode k_moveLeft;

    public KeyCode k_swap;
	public KeyCode k_operate;


    //player variables
    float movmentSpeed = 5f;

	//Rigidbody2D
    Rigidbody2D rb;


	// Use this for initialization
	void Start ()
    {
		DontDestroyOnLoad (this.gameObject);
		DontDestroyOnLoad (this);

		//Level1 Obstacles
		level1_door1 = GameObject.Find ("level1_door1");

		//Level1 Guards
		level1_guard1 = GameObject.Find("level1_guard1");
		level1_guard2 = GameObject.Find("level1_guard2");
		guards = new GameObject[] { level1_guard1, level1_guard2 };

		//Take-over mechanic
		takeoverTimer = 5;

		//Misc variables
        rb = GetComponent<Rigidbody2D>();
        controlObject = this.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (controlObject != this)
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        MovementController(controlObject);
        SwapController();
		OperationController();
	}

    void ChangeControlObject(GameObject obj)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		controlObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
        rb.velocity = new Vector2(0, 0);
        controlObject = obj;
        rb = obj.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        if (obj != this.gameObject)
        {
            StartCoroutine(controlTimer(takeoverTimer));
        }

		/*
		 * Note from Jodey Gee:
		 * 
		 * Upon exiting from the guard, have the players position set to the 
		 * guards position, this will be the MC 'exiting' the guards body. 
		 * 
		 */
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

    void SwapController()
    {
        if (Input.GetKeyDown(k_swap))
        {
            for (int i = 0; i < guards.Length; i++)
            {
                if (Vector2.Distance(guards[i].transform.position, transform.position) <= 2.5f)
                {
                    ChangeControlObject(guards[i].gameObject);
                }
            }
        }
    }

	void OnTriggerEnter2D(Collider2D colObj)
	{	
		//Obtain level1_key1
		if (colObj.name == "level1_key1" && controlObject == level1_guard1)
		{
			Destroy (colObj); 
			hasLevel1_key1 = true;
		}
	}

	void OperationController()
	{
		//Operations
		if (Input.GetKeyDown (k_operate))
		{
			//Open level1_door1
			if ( Vector2.Distance (controlObject.transform.position, level1_door1.transform.position) <= 2 && hasLevel1_key1 && controlObject == level1_guard1)
			{
				Destroy (level1_door1);
			}
		}
	}

	void OnTriggerStay2D(Collider2D colObj)
	{
		//if (colObj.gameObject.name == "level1Guard" && Input.GetKey(k_swap))
		//{
		//    ChangeControlObject(colObj.gameObject);
		//}
	}

    IEnumerator controlTimer (int controlTime)
    {
        yield return new WaitForSeconds(controlTime);
        ChangeControlObject(this.gameObject);
    }

}//end of class