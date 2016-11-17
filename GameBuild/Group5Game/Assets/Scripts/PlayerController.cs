using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //Global

    public Sprite leverUp;
    public Sprite leverPulled;

	// ------- //
	// Level 1 //
	// ------- //

	[Header("Level1 Objects")]
    //door
	public GameObject level1_door1Object;
	public GameObject level1_door2Object;
	public GameObject level1_door3Object;
    //lever
	public GameObject level1_lever1Object;

	[Header("Level1 Guards")]
	public GameObject level1_guard1;
	public GameObject level1_guard2;
	public GameObject level1_guard3;

	// ------- //
	// Level 2 //
	// ------- //

	[Header("Level2 Objects")]
    //door
	public GameObject level2_door1Object;
	public GameObject level2_door2Object;
	public GameObject level2_door3Object;
	public GameObject level2_door4Object;
	public GameObject level2_door5Object;
    //lever
    public GameObject level2_lever1Object;
    public GameObject level2_lever2Object;
    public GameObject level2_lever3Object;
    public GameObject level2_lever4Object;
    //Spikes
    public GameObject level2_spikes1Object;

    [Header("Level2 Guards")]
    public GameObject level2_guard1;
    public GameObject level2_guard2;
    public GameObject level2_guard3;

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
    int charges = 4;

    [Header("Diary variables")]
    public bool hasDiary1;
    public bool hasDiary2;

    [Header("Misc")]
    //Sprites
    public Sprite playerSprite;
    public Sprite emptySprite;

    //Rigidbody2D
    Rigidbody2D rb;

    [Header("Dont touch these variables")]
	public int level1_guard1_xScale = 4; public float level1_guard1_yScale = 3.5f;

	//Game bools
	public bool hasLevel1_key1 = false;
	public bool hasLevel1_key2 = false;

    public bool hasLevel2_key1 = false;
    public bool hasLevel2_key2 = false;

	//Take-over mechanic
	float takeoverTimer;
	public GameObject controlObject;
    GameObject player;

    //Misc
    float doorDistance = 3.5f;
    float leverDistance = 2f;

    //Canvas
    public GameObject canvas;
    CanvasController cc;


	// Use this for initialization
	void Start ()
    {
		DontDestroyOnLoad (this.gameObject);
		DontDestroyOnLoad (this);

        hasDiary1 = false; hasDiary2 = false;

        guards = new GameObject[] { level1_guard1, level1_guard2, level1_guard3, level2_guard1, level2_guard2, level2_guard3 };

		//Take-over mechanic
		takeoverTimer = 5;

		//Misc variables
        rb = GetComponent<Rigidbody2D>();
        controlObject = this.gameObject;

        player = this.gameObject;

        cc = canvas.GetComponent<CanvasController>();
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

    void TakeOverGuard(GameObject obj)
    {
        //Stop moving
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        controlObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        //Set control object
        controlObject = obj;

        //Make the player invisible
        player.GetComponent<SpriteRenderer>().sprite = emptySprite;

        //Set new rb
        rb = obj.GetComponent<Rigidbody2D>();

        //Stop moving
        rb.velocity = new Vector2(0, 0);

        //If taking over guard, start coroutine
        if (obj != this.gameObject)
        {
            StartCoroutine(controlTimer(takeoverTimer));
        }
    }

    void ReturnToPlayer()
    {
        //Stop moving
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        controlObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        //Call guard method
        controlObject.GetComponent<guardController>().returnToClosestWaypoint();

        //If the guard isn't normally patrolling (So they are in a room)
        if (controlObject.GetComponent<guardController>().getCurrentRoom() != "normal")
        {
            //Set the temPos of the guard to 0.
            controlObject.GetComponent<guardController>().setTempPos(0);
        }

        //Make the player visible again
        player.GetComponent<SpriteRenderer>().sprite = playerSprite;

        //Move the player to the guard
        player.transform.position = controlObject.transform.position;

        //Set control object
        controlObject = player;

        //Set new rb
        rb = player.GetComponent<Rigidbody2D>();
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
            else
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
            else
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
            else
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
            else
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
            else
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
            else
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
                    if (charges > 0)
                    {
                        charges--;
                        cc.updateCharges();
                        TakeOverGuard(guards[i].gameObject);
                    }
                    
                }
            }
        }
    }

	void OperationController()
	{
		//Guard operations
		if (Input.GetKeyDown (k_operate))
		{
            //Open level1_door1
            if (level1_door1Object)
            {
                if (Vector2.Distance(controlObject.transform.position, level1_door1Object.transform.position) <= doorDistance && hasLevel1_key1 && controlObject == level1_guard1)
                {
                    Destroy(level1_door1Object);
                    return;
                }
            }
            //Open level1_door2
            if (level1_door2Object)
            {
                if (Vector2.Distance(controlObject.transform.position, level1_door2Object.transform.position) < doorDistance && hasLevel1_key2 && controlObject == level1_guard2)
                {
                    Destroy(level1_door2Object);
                    return;
                }
            }

            //Operate level1_lever1
            if (level1_door3Object)
            {
                if (Vector2.Distance(controlObject.transform.position, level1_lever1Object.transform.position) < leverDistance && controlObject != this.gameObject)
                {
                    Destroy(level1_door3Object);
                    level1_lever1Object.GetComponent<SpriteRenderer>().sprite = leverPulled;
                    return;
                }
            }

            //Operate level2_lever1
            if (level2_door1Object)
            {
                if (Vector2.Distance(controlObject.transform.position, level2_lever1Object.transform.position) < leverDistance && controlObject != this.gameObject)
                {
                    Destroy(level2_door1Object);
                    level2_lever1Object.GetComponent<SpriteRenderer>().sprite = leverPulled;
                    return;
                }
            }
            //Open level2_door2
            if (level2_door2Object)
            {
                if (Vector2.Distance(controlObject.transform.position, level2_door2Object.transform.position) < doorDistance && controlObject == level2_guard2 && hasLevel2_key1)
                {
                    Destroy(level2_door2Object);
                    return;
                }
            }
            //Operate level2_lever2
            if (level2_door3Object)
            {
                if (Vector2.Distance(controlObject.transform.position, level2_lever2Object.transform.position) < leverDistance && controlObject != this.gameObject)
                {
                    Destroy(level2_door3Object);
                    level2_lever2Object.GetComponent<SpriteRenderer>().sprite = leverPulled;
                    return;
                }
            }
            //Operate level2_lever3
            if (level2_door4Object)
            {
                if (Vector2.Distance(controlObject.transform.position, level2_lever3Object.transform.position) < leverDistance && controlObject != this.gameObject)
                {
                    Destroy(level2_door4Object);
                    level2_lever3Object.GetComponent<SpriteRenderer>().sprite = leverPulled;
                    return;
                }
            }
            //Operate level2_lever4
            if (level2_spikes1Object)
            {
                if (Vector2.Distance(controlObject.transform.position, level2_lever4Object.transform.position) < leverDistance && controlObject != this.gameObject)
                {
                    Destroy(level2_spikes1Object);
                    level2_lever4Object.GetComponent<SpriteRenderer>().sprite = leverPulled;
                    return;
                }
            }
            //Open level2_door5
            if (level2_door5Object)
            {
                if (Vector2.Distance(controlObject.transform.position, level2_door5Object.transform.position) < doorDistance && controlObject == level2_guard3 && hasLevel2_key2)
                {
                    Destroy(level2_door5Object);
                    return;
                }
            }
            //Next object...
		}
	}


    IEnumerator controlTimer (float controlTime)
    {
        cc.timerScrollSize = getTakeoverTimer();

        float temp = controlTime;

        temp *= 10;

        for (int i = 0; i < temp; i++)
        {
            cc.timerScrollSize -= 0.1f;
            cc.updateTakeoverBar();
            yield return new WaitForSeconds(0.1f);
        }

        cc.timerScrollSize = takeoverTimer;
        cc.updateTakeoverBar();
        ReturnToPlayer();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (charges < 4 && target.gameObject.tag == "chargePickup")
        {
            Destroy(target.gameObject);
            charges++;
            cc.updateCharges();
            return;
        }

        if (target.gameObject.name == "diary1")
        {
            hasDiary1 = true;
            Destroy(target.gameObject);
            cc.updateInventory();
            return;
        }
        else if (target.gameObject.name == "diary2")
        {
            hasDiary2 = true;
            Destroy(target.gameObject);
            cc.updateInventory();
            return;
        }
    }

    public int getCharges()
    {
        return charges;
    }
    public float getTakeoverTimer()
    {
        return takeoverTimer;
    }


}//end of class