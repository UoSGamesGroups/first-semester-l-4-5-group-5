using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingController : MonoBehaviour
{
    GameObject player;
    PlayerController pc;

    float kingScale;

    public GameObject[] wayPointsArray;

    Rigidbody2D rb;
    float moveSpeed = 2f;

    private int currPos;

    int currentState;
    int health;

    public Sprite kingHurt;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        kingScale = 4;
        currPos = 0;
        currentState = 1;
        health = 2;

	}
	
	// Update is called once per frame
	void Update ()
    {
		//State - roam
        if (currentState == 1)
        {
            MoveToWayponts();
        }
        //State - dead
        if (currentState == 2)
        {
            /* no op */
        }
    }

    void MoveToWayponts()
    {
        Vector2 movePos = new Vector2(wayPointsArray[currPos].transform.position.x - transform.position.x, wayPointsArray[currPos].transform.position.y - transform.position.y);
        rb.velocity = movePos.normalized * moveSpeed;

        //Guard facing left or right
        if (movePos.x < 0)
            transform.localScale = new Vector2(-kingScale, kingScale);
        else
            transform.localScale = new Vector2(kingScale, kingScale);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        //--------------------------
        //Walking into waypoints

        //Move to the next waypoint
        for (int i = 0; i < wayPointsArray.Length; i++)
        {
            if (target.gameObject == wayPointsArray[i] && target.gameObject != wayPointsArray[wayPointsArray.Length - 1])
            {
                currPos++;
                return;
            }
        }
        //If end of the array
        if (target.gameObject == wayPointsArray[wayPointsArray.Length - 1])
        {
            //Loop around again
            currPos = 0;
            return;
        }

        if (target.gameObject.name == "childTrig")
        {
            target.gameObject.SetActive(false);
            takeDamage();
        }
    }

    void takeDamage()
    {
        health--;
        Debug.Log("King health: " + health);
        if (health == 1)
            GetComponent<SpriteRenderer>().sprite = kingHurt;
        if (health == 0)
            die();
    }

    void die()
    {
        currentState = 2;
        transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        rb.velocity = new Vector2(0, 0);
        Destroy(this.GetComponent<BoxCollider2D>());
    }

}
