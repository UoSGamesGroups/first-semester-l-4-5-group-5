﻿using UnityEngine;
using System.Collections;

public class level1_guard1 : MonoBehaviour
{

    GameObject player;
    PlayerController pc;

    GameObject[] wayPointsArray;
    public GameObject waypoint1;
	public GameObject waypoint2;
	public GameObject waypoint3;
	public GameObject waypoint4;

    Rigidbody2D rb;
    float moveSpeed = 2f;

    int currPos = 0;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerController>();

        /*waypoint1 = GameObject.Find("firstRoomTopLeft");
        waypoint2 = GameObject.Find("firstRoomTopRight");
        waypoint3 = GameObject.Find("firstRoomBottomRight");
        waypoint4 = GameObject.Find("firstRoomBottomLeft");*/

        wayPointsArray = new GameObject[] { waypoint1, waypoint2, waypoint3, waypoint4 };
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (pc.controlObject != this.gameObject)
            moveToWaypoints();
	}

    void moveToWaypoints()
    {
        Vector2 movePos = new Vector2(wayPointsArray[currPos].transform.position.x - transform.position.x, wayPointsArray[currPos].transform.position.y - transform.position.y);

        rb.velocity = movePos.normalized * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D target)
    {

        if (target.name == "firstRoomTopLeft" || target.name == "firstRoomTopRight" || target.name == "firstRoomBottomRight")
        {
            currPos++;
        }
        else
            currPos = 0;

		//Player
		//Obtain level1_key1
		if (pc.controlObject == this.gameObject)
		{
			Debug.Log ("colObj: " + target.name);

			if (target.name == "level1_key1")
			{
				Destroy (target.gameObject);
				pc.hasLevel1_key1 = true;
			}
		}


    }

}
