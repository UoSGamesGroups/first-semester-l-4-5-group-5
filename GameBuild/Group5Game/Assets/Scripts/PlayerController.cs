using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	//Global
	int level = 1;

	// ------- //
	// Level 1 //
	// ------- //

	[Header("Level1 Objects")]
	public GameObject level1_door1Object;
	public GameObject level1_door2Object;
	public GameObject level1_door3Object;
	public GameObject level1_lever1Object;

	[Header("Level1 Guards")]
	public GameObject level1_guard1;
	public GameObject level1_guard2;
	public GameObject level1_guard3;



	// ------- //
	// Level 2 //
	// ------- //

	[Header("Level2 Objects")]
	public GameObject level2_door1Object;
	public GameObject level2_door2Object;
	public GameObject level2_door3Object;
	public GameObject level2_door4Object;
	public GameObject level2_door5Object;
	public GameObject level2_door6Object;

	// ------- //
	//  Global //
	// ------- //

	GameObject[] guards;

	[Header("Input control")]
    public KeyCode k_moveUp;
    public KeyCode k_moveRight;
    public KeyCode k_moveDown;
    public KeyCode k_moveLeft;

    public KeyCode k_swap;
	public KeyCode k_operate;

    //player variables
    float movmentSpeed = 5f;
    int xScale = 3;
    float yScale = 2.5f;

    //Rigidbody2D
    Rigidbody2D rb;

	[Header("Dont touch these variables")]
	public int level1_guard1_xScale = 4; public float level1_guard1_yScale = 3.5f;

	//Game bools
	public bool hasLevel1_key1 = false;
	public bool hasLevel1_key2 = false;

	//Take-over mechanic
	int takeoverTimer;
	public GameObject controlObject;


	// Use this for initialization
	void Start ()
    {
		DontDestroyOnLoad (this.gameObject);
		DontDestroyOnLoad (this);

		guards = new GameObject[] { level1_guard1, level1_guard2, level1_guard3 };

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

            //Player
            if (controlObject == this.gameObject)
                controlObject.transform.localScale = new Vector2(-xScale, yScale);
            //level1guards
            else if (controlObject == level1_guard1.gameObject || controlObject == level1_guard2.gameObject || controlObject == level1_guard3.gameObject)
                controlObject.transform.localScale = new Vector2(-level1_guard1_xScale, level1_guard1_yScale);
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

            //Player
            if (controlObject == this.gameObject)
                controlObject.transform.localScale = new Vector2(xScale, yScale);
            //level1guards
            else if (controlObject == level1_guard1.gameObject || controlObject == level1_guard2.gameObject || controlObject == level1_guard3.gameObject)
                controlObject.transform.localScale = new Vector2(level1_guard1_xScale, level1_guard1_yScale);
        }
        //Right
        else if (Input.GetKey(k_moveRight) && !Input.GetKey(k_moveUp) && !Input.GetKey(k_moveDown)) //if right and not up or down
        {
			rb.velocity = new Vector2(movmentSpeed, 0);

            //Player
            if (controlObject == this.gameObject)
                controlObject.transform.localScale = new Vector2(xScale, yScale);
            //level1guards
            else if (controlObject == level1_guard1.gameObject || controlObject == level1_guard2.gameObject || controlObject == level1_guard3.gameObject)
                controlObject.transform.localScale = new Vector2(level1_guard1_xScale, level1_guard1_yScale);
        }
        //Down-Right
        else if (Input.GetKey(k_moveDown) && Input.GetKey(k_moveRight))
        {
			rb.velocity = new Vector2(movmentSpeed, -movmentSpeed);

            //Player
            if (controlObject == this.gameObject)
                controlObject.transform.localScale = new Vector2(xScale, yScale);
            //level1guards
            else if (controlObject == level1_guard1.gameObject || controlObject == level1_guard2.gameObject || controlObject == level1_guard3.gameObject)
                controlObject.transform.localScale = new Vector2(level1_guard1_xScale, level1_guard1_yScale);
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

            //Player
            if (controlObject == this.gameObject)
                controlObject.transform.localScale = new Vector2(-xScale, yScale);
            //level1guards
            else if (controlObject == level1_guard1.gameObject || controlObject == level1_guard2.gameObject || controlObject == level1_guard3.gameObject)
                controlObject.transform.localScale = new Vector2(-level1_guard1_xScale, level1_guard1_yScale);
        }
        //Left
        else if (Input.GetKey(k_moveLeft) && !Input.GetKey(k_moveUp) && !Input.GetKey(k_moveDown)) //if left and not up or down
        {
			rb.velocity = new Vector2(-movmentSpeed, 0);

            //Player
            if (controlObject == this.gameObject)
                controlObject.transform.localScale = new Vector2(-xScale, yScale);
            //level1guards
            else if (controlObject == level1_guard1.gameObject || controlObject == level1_guard2.gameObject || controlObject == level1_guard3.gameObject)
                controlObject.transform.localScale = new Vector2(-level1_guard1_xScale, level1_guard1_yScale);
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

	void OperationController()
	{
		//Operations
		if (Input.GetKeyDown (k_operate))
		{
            //Open level1_door1
            if (level1_door1Object)
            {
                if (Vector2.Distance(controlObject.transform.position, level1_door1Object.transform.position) <= 3.5 && hasLevel1_key1 && controlObject == level1_guard1)
                {
                    Destroy(level1_door1Object);
                }
            }
            //Open level1_door2
            if (level1_door2Object)
            {
                if (Vector2.Distance(controlObject.transform.position, level1_door2Object.transform.position) < 3.5 && hasLevel1_key2 && controlObject == level1_guard2)
                {
                    Destroy(level1_door2Object);
                }
            }
            //Open level1_door3
            if (level1_door3Object)
            {
                if (Vector2.Distance(controlObject.transform.position, level1_lever1Object.transform.position) < 3 && controlObject == level1_guard3)
                {
                    Destroy(level1_door3Object);
                    level1_lever1Object.transform.localScale = new Vector2(-level1_lever1Object.transform.localScale.x, level1_lever1Object.transform.localScale.y);
                    Debug.Log("level1_guard3 Unlocking level1_door3 by using level1_lever1");
                }
            }
            //Next object...
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