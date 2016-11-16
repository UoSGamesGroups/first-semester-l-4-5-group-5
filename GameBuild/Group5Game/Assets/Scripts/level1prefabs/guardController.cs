﻿using UnityEngine;
using System.Collections;

public class guardController : MonoBehaviour
{
    
    GameObject player;
    PlayerController pc;

    public GameObject[] wayPointsArray;

    Rigidbody2D rb;
    float moveSpeed = 2f;

    private int currPos;


	string currentRoom;
	GameObject[] tempArray;
	int tempPos;

	GameObject camera;
	LevelController lc;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerController>();
		rb = GetComponent<Rigidbody2D>();

		currPos = 0;
		tempPos = 0;

		currentRoom = "normal";

		camera = GameObject.FindGameObjectWithTag("MainCamera");
		lc = camera.GetComponent<LevelController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (pc.controlObject != this.gameObject)
		{
			if (currentRoom == "normal")
			{
				moveToWaypoints();
			}
			else
			{
				exitRoom();
			}
		}
	}

    void moveToWaypoints()
    {
        Vector2 movePos = new Vector2(wayPointsArray[currPos].transform.position.x - transform.position.x, wayPointsArray[currPos].transform.position.y - transform.position.y);
        rb.velocity = movePos.normalized * moveSpeed;

        //Guard facing left or right
        if (movePos.x < 0)
            transform.localScale = new Vector2(-pc.level1_guard1_xScale, pc.level1_guard1_yScale);
        else
            transform.localScale = new Vector2(pc.level1_guard1_xScale, pc.level1_guard1_yScale);
    }

	void exitRoom()
	{
		//code
		Vector2 movePos = new Vector2(tempArray[tempPos].transform.position.x - transform.position.x, tempArray[tempPos].transform.position.y - transform.position.y);
		rb.velocity = movePos.normalized * moveSpeed;

        for (int i = 0; i < tempArray.Length; i++)
        {
            Debug.Log("tempArray " + i + ": " + tempArray[i].gameObject.name);
        }
        Debug.Log("tempPos:" + tempPos);
        //Debug.Log("Guard: " + this.gameObject.name + " Walking to: " + tempArray[tempPos].gameObject.name);

		//Guard facing left or right
		if (movePos.x < 0)
			transform.localScale = new Vector2(-pc.level1_guard1_xScale, pc.level1_guard1_yScale);
		else
			transform.localScale = new Vector2(pc.level1_guard1_xScale, pc.level1_guard1_yScale);
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if (pc.controlObject != this.gameObject)
        {
            //The guard is just normally walking
            if (currentRoom == "normal")
            {
                //Move to the next waypoint
                for (int i = 0; i < wayPointsArray.Length; i++)
                {
                    if (target.gameObject == wayPointsArray[i] && target.gameObject != wayPointsArray[wayPointsArray.Length - 1])
                    {
                        currPos++;
                        return;
                    }
                }
                //If level1Guard reaches end of the array
                if (target.gameObject == wayPointsArray[wayPointsArray.Length - 1] && gameObject.tag != "level2Guards")
                {
                    //Loop around again
                    currPos = 0;
                    return;
                }
                //Else if level2guard reaches end of the array (Reverse the array)
                else if (target.gameObject == wayPointsArray[wayPointsArray.Length - 1] && gameObject.tag == "level2Guards")
                {

                    //Swap the array around
                    for (int i = 0; i < wayPointsArray.Length / 2; i++)
                    {
                        GameObject temp = wayPointsArray[i];
                        wayPointsArray[i] = wayPointsArray[(wayPointsArray.Length - i) - 1];
                        wayPointsArray[(wayPointsArray.Length - i) - 1] = temp;
                    }
                    currPos = 1;
                }
            }
            ///////////////////////////////
            //WAYPOINTS FOR EXITING ROOMS//
            ///////////////////////////////
            else //(If guard isn't patrolling)
            {
                //For all waypoints
                for (int i = 0; i < tempArray.Length; i++)
                {
                    //If you bump into your current target and this isn't the last waypoint
                    if (target.gameObject == tempArray[i] && target.gameObject != tempArray[tempArray.Length - 1])
                    {
                        //Move to the next one
                        tempPos++;
                        return;
                    }
                }
                //If you reach the end of the temporary array
                if (target.gameObject == tempArray[tempArray.Length - 1])
                {
                    //return to normal
                    currentRoom = "normal";
                    returnToClosestWaypoint();
                }

            }
        }

        //////////////////
		//Obtaining keys//
        //////////////////
		if (pc.controlObject == this.gameObject)
		{
            //Obtain level1_key1
            if (this.gameObject.name == "level1_guard1" && target.name == "level1_key1")
			{
				Destroy (target.gameObject);
                //Debug.Log("level1_guard1 collected level1_key1");
				pc.hasLevel1_key1 = true;
				return;
			}

            //Obtain level1_key2
            else if (this.gameObject.name == "level1_guard2" && target.name == "level1_key2")
            {
                Destroy(target.gameObject);
                //Debug.Log("level1_guard2 collected level1_key2");
                pc.hasLevel1_key2 = true;
                return;
            }

            //Obtain level2_key1
            else if (this.gameObject.name == "level2_guard2" && target.name == "level2_key1")
            {
                Destroy(target.gameObject);
                pc.hasLevel2_key1 = true;
                return;
            }

            //Obtain level2_key2
            else if (this.gameObject.name == "level2_guard3" && target.name == "level2_key2")
            {
                Destroy(target.gameObject);
                pc.hasLevel2_key2 = true;
                return;
            }
            //Next object...
        }


        /////////////////////
		//WALKING INTO ROOM//
        /////////////////////
		//level1
		if (target.gameObject.name == "bottomRoomCollider" && this.gameObject.name != "level1_guard1")
		{
            //Debug.Log("Guard: " + this.gameObject.name + " Walked into: " + target.gameObject.name);
			currentRoom = "level1bottomRoom";
			tempArray = lc.level1bottomArray;
			tempPos = 0;
			return;
		}
		if (target.gameObject.name == "secondRoomCollider" && this.gameObject.name == "level1_guard1")
		{
            //Debug.Log("Guard: " + this.gameObject.name + " Walked into: " + target.gameObject.name);
            currentRoom = "level1middleRoom";
            tempArray = lc.level1middleArrayDown;
			tempPos = 0;
			return;
		}
		if (target.gameObject.name == "secondRoomCollider" && this.gameObject.name == "level1_guard3")
		{
            //Debug.Log("Guard: " + this.gameObject.name + " Walked into: " + target.gameObject.name);
            currentRoom = "level1middleRoom";
            tempArray = lc.level1middleArrayUp;
			tempPos = 0;
			return;
		}
		if (target.gameObject.name == "thirdRoomCollider" && this.gameObject.name != "level1_guard3")
		{
            //Debug.Log("Guard: " + this.gameObject.name + " Walked into: " + target.gameObject.name);
            currentRoom = "level1topRoom";
            tempArray = lc.level1topLeftArray;
			tempPos = 0;
			return;
		}

		switch (target.gameObject.name)
		{
		//level2
		case "level2bottomLeft":
            currentRoom = "level2bottomLeft";
            tempArray = lc.level2bottomLeftArray;
			tempPos = 0;
            break;
		case "level2bottomRight":
			currentRoom = "level2bottomRight";
            tempArray = lc.level2bottomRightArray;
			tempPos = 0;
            break;
		case "level2right":
			currentRoom = "level2right";
            tempArray = lc.level2rightArray;
			tempPos = 0;
            break;
		case "level2topRight":
			currentRoom = "level2topRight";
            tempArray = lc.level2topRightArray;
			tempPos = 0;
            break;
		case "level2top":
			currentRoom = "level2top";
            tempArray = lc.level2topArray;
			tempPos = 0;
            break;
		case "level2topLeft":
			currentRoom = "level2topLeft";
            tempArray = lc.level2topLeftArray;
			tempPos = 0;
            break;
		case "level2left":
			currentRoom = "level2left";
            tempArray = lc.level2leftArray;
			tempPos = 0;
            break;
		case "level2middle":
			currentRoom = "level2middle";
            tempArray = lc.level2middleArray;
			tempPos = 0;
            break;
		}
    }//End OnTriggerEnter2D

    void OnTriggerExit2D(Collider2D target)
    {
        if (pc.controlObject == this.gameObject)
        {
            if (target.gameObject.name == "bottomRoomCollider")
            {
                currentRoom = "normal";
                return;
            }
            if (target.gameObject.name == "secondRoomCollider")
            {
                currentRoom = "normal";
                return;
            }
            if (target.gameObject.name == "secondRoomCollider")
            {
                currentRoom = "normal";
                return;
            }
            if (target.gameObject.name == "thirdRoomCollider")
            {
                currentRoom = "normal";
                return;
            }

            switch (target.gameObject.name)
            {
                //level2
                case "level2bottomLeft":
                    currentRoom = "normal";
                    break;
                case "level2bottomRight":
                    currentRoom = "normal";
                    break;
                case "level2right":
                    currentRoom = "normal";
                    break;
                case "level2topRight":
                    currentRoom = "normal";
                    break;
                case "level2top":
                    currentRoom = "normal";
                    break;
                case "level2topLeft":
                    currentRoom = "normal";
                    break;
                case "level2left":
                    currentRoom = "normal";
                    break;
                case "level2middle":
                    currentRoom = "normal";
                    break;
            }
        }
    }

    public void returnToClosestWaypoint()
    {
        Debug.Log("returnToClosestWaypoint: " + this.gameObject.name);
        float shortestDist = Vector2.Distance(this.gameObject.transform.position, wayPointsArray[0].gameObject.transform.position);
        currPos = 0;
        //Debug.Log(wayPointsArray[0].gameObject.name + ": " + shortestDist);
        float tempDist;
        for (int i = 1; i < wayPointsArray.Length; i++)
        {
            tempDist = Vector2.Distance(this.gameObject.transform.position, wayPointsArray[i].gameObject.transform.position);
            //Debug.Log(wayPointsArray[i].gameObject.name + ": " + tempDist);

            if (tempDist < shortestDist)
            {
                shortestDist = tempDist;
                currPos = i;
            }
                
            //Debug.Log("Loop " + i + "// Shortest dist : " + shortestDist);
        }

        //Debug.Log("Returning to: " + wayPointsArray[currPos].gameObject.name);
    }//End returnToClosestWaypoint

    public string getCurrentRoom()
    {
        return currentRoom;
    }
    
    public void setTempPos(int a)
    {
        tempPos = a;
    }
}
