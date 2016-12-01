using UnityEngine;
using System.Collections;

public class guardController : MonoBehaviour
{
    
    GameObject player;
    PlayerController pc;

    public GameObject[] wayPointsArray;

    Rigidbody2D rb;
    float moveSpeed = 2f;
    public int takeOverTime;

    private int currPos;

    public GameObject childKeyObj;

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
                //Debug.Log(this.gameObject.name + " != normal room");
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
            //Debug.Log("tempArray " + i + ": " + tempArray[i].gameObject.name);
        }
        //Debug.Log("tempPos:" + tempPos);
        //Debug.Log("Guard: " + this.gameObject.name + " Walking to: " + tempArray[tempPos].gameObject.name);

		//Guard facing left or right
		if (movePos.x < 0)
			transform.localScale = new Vector2(-pc.level1_guard1_xScale, pc.level1_guard1_yScale);
		else
			transform.localScale = new Vector2(pc.level1_guard1_xScale, pc.level1_guard1_yScale);
	}

    public void activateChildObject(bool foo)
    {
        if (foo)
        {
            childKeyObj.SetActive(true);
        }
        else if (!foo)
        {
            childKeyObj.SetActive(false);
        }
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
            ///////////////////////////////////
            ////WAYPOINTS FOR EXITING ROOMS////
            ///////////////////////////////////
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

            ////level1////

            //Obtain level1_key1
            if (this.gameObject.name == "level1_guard1" && target.name == "level1_key1")
			{
				Destroy (target.gameObject);
                //Debug.Log("level1_guard1 collected level1_key1");
                lc.playSound("keyPickup");
				pc.hasLevel1_key1 = true;
				return;
			}

            //Obtain level1_key2
            else if (this.gameObject.name == "level1_guard2" && target.name == "level1_key2")
            {
                Destroy(target.gameObject);
                //Debug.Log("level1_guard2 collected level1_key2");
                lc.playSound("keyPickup");
                pc.hasLevel1_key2 = true;
                return;
            }

            ////levelnew////

            //Obtain levelNew_key1
            else if (this.gameObject.name == "levelNew_guard1" && target.name == "levelNew_key1")
            {
                Destroy(target.gameObject);
                lc.playSound("keyPickup");
                pc.hasLevelNew_key1 = true;
                return;
            }

            //Obtain levelNew_key2
            else if (this.gameObject.name == "levelNew_guard2" && target.name == "levelNew_key2")
            {
                Destroy(target.gameObject);
                lc.playSound("keyPickup");
                pc.hasLevelNew_key2 = true;
                return;
            }

            //Obtain levelNew_key3
            else if (this.gameObject.name == "levelNew_guard2" && target.name == "levelNew_key3")
            {
                Destroy(target.gameObject);
                lc.playSound("keyPickup");
                pc.hasLevelNew_key3 = true;
                return;
            }

            ////level2////

            //Obtain level2_key1
            else if (this.gameObject.name == "level2_guard2" && target.name == "level2_key1")
            {
                Destroy(target.gameObject);
                lc.playSound("keyPickup");
                pc.hasLevel2_key1 = true;
                return;
            }

            //Obtain level2_key2
            else if (this.gameObject.name == "level2_guard3" && target.name == "level2_key2")
            {
                Destroy(target.gameObject);
                lc.playSound("keyPickup");
                pc.hasLevel2_key2 = true;
                return;
            }

            //Obtain level3_key1
            else if (this.gameObject.name == "level3_guard1" && target.name == "level3_key1")
            {
                Destroy(target.gameObject);
                lc.playSound("keyPickup");
                pc.hasLevel3_key1 = true;
                return;
            }

            //Obtain level3_key2
            else if (this.gameObject.name == "level3_guard1" && target.name == "level3_key2")
            {
                Destroy(target.gameObject);
                lc.playSound("keyPickup");
                pc.hasLevel3_key2 = true;
                return;
            }

            //Obtain level3_key3
            else if (this.gameObject.name == "level3_guard4" && target.name == "level3_key3")
            {
                Destroy(target.gameObject);
                lc.playSound("keyPickup");
                pc.hasLevel3_key3 = true;
                return;
            }

            //Obtain level3_key4
            else if (this.gameObject.name == "level3_guard4" && target.name == "level3_key4")
            {
                Destroy(target.gameObject);
                lc.playSound("keyPickup");
                pc.hasLevel3_key4 = true;
                return;
            }

            //Obtain level3_key5
            else if (this.gameObject.name == "level3_guard5" && target.name == "level3_key5")
            {
                Destroy(target.gameObject);
                lc.playSound("keyPickup");
                pc.hasLevel3_key5 = true;
                return;
            }

            //Obtain level3_key6
            else if (this.gameObject.name == "level3_guard6" && target.name == "level3_key6")
            {
                Destroy(target.gameObject);
                lc.playSound("keyPickup");
                pc.hasLevel3_key6 = true;
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
            Debug.Log("Guard: " + this.gameObject.name + " Walked into: " + target.gameObject.name);
			currentRoom = "level1bottomRoom";
			tempArray = lc.level1bottomArray;
			tempPos = 0;
			return;
		}
		if (target.gameObject.name == "secondRoomCollider" && this.gameObject.name == "level1_guard1")
		{
            Debug.Log("Guard: " + this.gameObject.name + " Walked into: " + target.gameObject.name);
            currentRoom = "level1middleRoom";
            tempArray = lc.level1middleArrayDown;
			tempPos = 0;
			return;
		}
		if (target.gameObject.name == "secondRoomCollider" && this.gameObject.name == "level1_guard3")
		{
            Debug.Log("Guard: " + this.gameObject.name + " Walked into: " + target.gameObject.name);
            currentRoom = "level1middleRoom";
            tempArray = lc.level1middleArrayUp;
			tempPos = 0;
			return;
		}
		if (target.gameObject.name == "thirdRoomCollider" && this.gameObject.name != "level1_guard3")
		{
            Debug.Log("Guard: " + this.gameObject.name + " Walked into: " + target.gameObject.name);
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

            //LevelNew
            case "levelNew_leftTop":
                currentRoom = "levelNewLeftTop";
                tempArray = lc.levelnewLeftTopArray;
                tempPos = 0;
                break;
            case "levelNew_leftBottom":
                currentRoom = "levelNewLeftBottom";
                tempArray = lc.levelnewLeftBottomArray;
                tempPos = 0;
                break;
            case "levelNew_topRight":
                currentRoom = "levelNewTopRight";
                tempArray = lc.levelnewTopRightArray;
                tempPos = 0;
                break;
            case "levelNew_topLeft":
                currentRoom = "levelNewTopLeft";
                tempArray = lc.levelnewTopLeftArray;
                tempPos = 0;
                break;
            case "levelNew_northRoom":
                currentRoom = "levelNewNorth";
                tempArray = lc.levelnewNorthRoomArray;
                tempPos = 0;
                break;
            case "levelNew_southRoom":
                if (this.gameObject.name == "levelNew_guard2")
                {
                    currentRoom = "levelNewSouthRoom";
                    tempArray = lc.levelnewSouthRoomArray;
                    tempPos = 0;
                }
                break;
            case "levelNew_mainRoomCollider":
                if (this.gameObject.name == "levelNew_guard1")
                {
                    currentRoom = "levelNewMainRoom";
                    tempArray = lc.levelnewMainRoomArray;
                    tempPos = 0;
                }
                break;
            default:
                break;
        }

        //level3
        if (target.gameObject.name == "level3NorthLeft" && this.gameObject.name == "level3_guard3")
        {
            currentRoom = "level3Northleft";
            tempArray = lc.level3NorthLeftArray;
            tempPos = 0;
        }
        else if (target.gameObject.name == "level3North" && (this.gameObject.name == "level3_guard2" || this.gameObject.name == "level3_guard1"))
        {
            currentRoom = "level3North";
            tempArray = lc.level3NorthArray;
            tempPos = 0;
        }
        else if (target.gameObject.name == "level3RightNorth")
        {
            currentRoom = "level3RightNorth";
            tempArray = lc.level3RightNorthArray;
            tempPos = 0;
        }
        else if (target.gameObject.name == "level3Right" && (this.gameObject.name == "level3_guard2" || this.gameObject.name == "level3_guard1"))
        {
            currentRoom = "level3Right";
            tempArray = lc.level3MiddleRightArray;
            tempPos = 0;
        }
        else if (target.gameObject.name == "level3RightSouth")
        {
            currentRoom = "level3RightSouth";
            tempArray = lc.level3RightSouthArray;
            tempPos = 0;
        }
        else if (target.gameObject.name == "level3LeftNorth")
        {
            currentRoom = "level3LeftNorth";
            tempArray = lc.level3LeftNorthArray;
            tempPos = 0;
        }
        else if (target.gameObject.name == "level3Left" && (this.gameObject.name == "level3_guard2" || this.gameObject.name == "level3_guard1") )
        {
            currentRoom = "level3Left";
            tempArray = lc.level3LeftArray;
            tempPos = 0;
        }
        else if (target.gameObject.name == "level3LeftSouth" && this.gameObject.name == "level3_guard5")
        {
            currentRoom = "level3LeftSouth";
            tempArray = lc.level3LeftSouthArray;
            tempPos = 0;
        }
        else if (target.gameObject.name == "level3LeftSouthEast" && this.gameObject.name == "level3_guard6")
        {
            currentRoom = "level3LeftSouthEast";
            tempArray = lc.level3LeftSouthEastArray;
            tempPos = 0;
        }

        //Guards leaving middle
        if (target.gameObject.name == "level3Middle")
        {
            //north
            if (this.gameObject.name == "level3_guard3")
            {
                currentRoom = "level3Middle";
                tempArray = lc.level3MiddleNorthArray;
                tempPos = 0;
            }
            //left
            else if (this.gameObject.name == "level3_guard5")
            {
                currentRoom = "level3Middle";
                tempArray = lc.level3MiddleLeftArray;
                tempPos = 0;
            }
            //east
            else if (this.gameObject.name == "level3_guard4")
            {
                currentRoom = "level3Middle";
                tempArray = lc.level3MiddleRightArray;
                tempPos = 0;
            }
        }
    }//End OnTriggerEnter2D

    void OnTriggerExit2D(Collider2D target)
    {
        //if (pc.controlObject == this.gameObject)
        //{
        //    if (target.gameObject.name == "bottomRoomCollider")
        //    {
        //        currentRoom = "normal";
        //        return;
        //    }
        //    if (target.gameObject.name == "secondRoomCollider")
        //    {
        //        currentRoom = "normal";
        //        return;
        //    }
        //    if (target.gameObject.name == "secondRoomCollider")
        //    {
        //        currentRoom = "normal";
        //        return;
        //    }
        //    if (target.gameObject.name == "thirdRoomCollider")
        //    {
        //        currentRoom = "normal";
        //        return;
        //    }

        //    switch (target.gameObject.name)
        //    {
        //        //level2
        //        case "level2bottomLeft":
        //            currentRoom = "normal";
        //            break;
        //        case "level2bottomRight":
        //            currentRoom = "normal";
        //            break;
        //        case "level2right":
        //            currentRoom = "normal";
        //            break;
        //        case "level2topRight":
        //            currentRoom = "normal";
        //            break;
        //        case "level2top":
        //            currentRoom = "normal";
        //            break;
        //        case "level2topLeft":
        //            currentRoom = "normal";
        //            break;
        //        case "level2left":
        //            currentRoom = "normal";
        //            break;
        //        case "level2middle":
        //            currentRoom = "normal";
        //            break;
        //    }
        //}

        if (target.gameObject.name == "level3NorthLeft" && this.gameObject.name == "level3_guard3")
        {
            currentRoom = "normal";
            return;
        }
        else if (target.gameObject.name == "level3North" && (this.gameObject.name == "level3_guard2" || this.gameObject.name == "level3_guard1"))
        {
            currentRoom = "normal";
            return;
        }
        else if (target.gameObject.name == "level3RightNorth")
        {
            currentRoom = "normal";
            return;
        }
        else if (target.gameObject.name == "level3Right" && (this.gameObject.name == "level3_guard2" || this.gameObject.name == "level3_guard1"))
        {
            currentRoom = "normal";
            return;
        }
        else if (target.gameObject.name == "level3RightSouth")
        {
            currentRoom = "normal";
            return;
        }
        else if (target.gameObject.name == "level3LeftNorth")
        {
            currentRoom = "normal";
            return;
        }
        else if (target.gameObject.name == "level3Left" && (this.gameObject.name == "level3_guard2" || this.gameObject.name == "level3_guard1"))
        {
            currentRoom = "normal";
            return;
        }
        else if (target.gameObject.name == "level3LeftSouth" && this.gameObject.name == "level3_guard5")
        {
            currentRoom = "normal";
            return;
        }
        else if (target.gameObject.name == "level3LeftSouthEast" && this.gameObject.name == "level3_guard6")
        {
            currentRoom = "normal";
            return;
        }

        //Guards leaving middle
        if (target.gameObject.name == "level3Middle")
        {
            //north
            if (this.gameObject.name == "level3_guard3")
            {
                currentRoom = "normal";
                return;
            }
            //left
            else if (this.gameObject.name == "level3_guard5")
            {
                currentRoom = "normal";
                return;
            }
            //east
            else if (this.gameObject.name == "level3_guard4")
            {
                currentRoom = "normal";
                return;
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
