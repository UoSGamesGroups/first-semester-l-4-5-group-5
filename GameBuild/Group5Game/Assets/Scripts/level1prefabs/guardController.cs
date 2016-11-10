using UnityEngine;
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
	GameObject[] oldArray;
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
		Vector2 movePos = new Vector2(wayPointsArray[tempPos].transform.position.x - transform.position.x, wayPointsArray[currPos].transform.position.y - transform.position.y);
		rb.velocity = movePos.normalized * moveSpeed;

		//Guard facing left or right
		if (movePos.x < 0)
			transform.localScale = new Vector2(-pc.level1_guard1_xScale, pc.level1_guard1_yScale);
		else
			transform.localScale = new Vector2(pc.level1_guard1_xScale, pc.level1_guard1_yScale);
	}

    void OnTriggerEnter2D(Collider2D target)
    {

		//While normally walking
		if (currentRoom == "normal")
		{
			for (int i = 0; i < wayPointsArray.Length; i++)
			{
				if (target.gameObject == wayPointsArray[i] && target.gameObject != wayPointsArray[wayPointsArray.Length-1])
				{
					currPos++;
					return;
				}
			}
			//level1Guard reaches end of the array
			if (target.gameObject == wayPointsArray[wayPointsArray.Length -1] && gameObject.tag != "level2Guards")
			{
				currPos = 0;
				return;
			}
			//level 2 gurad reaches end of the array (Reverse the array)
			else if (target.gameObject == wayPointsArray[wayPointsArray.Length -1] && gameObject.tag == "level2Guards")
			{

				//Swap the array around
				for (int i = 0; i < wayPointsArray.Length /2; i++)
				{
					GameObject temp = wayPointsArray[i];
					wayPointsArray[i] = wayPointsArray[(wayPointsArray.Length -i) - 1];
					wayPointsArray[(wayPointsArray.Length -i) - 1] = temp;
				}
				currPos = 1;
			}
		}
		//Else if the guard is returning to another room
		else
		{
			//Move to next waypoint
			for (int i = 0; i < wayPointsArray.Length; i++)
			{
				if (target.gameObject == wayPointsArray[i] && target.gameObject != wayPointsArray[wayPointsArray.Length-1])
				{
					currPos++;
					return;
				}
			}
			//If you reach the end of the temporary array
			if (target.gameObject == wayPointsArray[wayPointsArray.Length -1])
			{
				//return to normal
				wayPointsArray = oldArray;
				currentRoom = "normal";
				returnToClosestWaypoint();
			}

		}

		//Player related
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


		//Walking into rooms

		//level1
		if (target.gameObject.name == "bottomRoomCollider" && this.gameObject.name != "level1_guard1")
		{
			currentRoom = "level1bottomRoom";
			oldArray = wayPointsArray;
			wayPointsArray = lc.level1bottomArray;
			tempPos = 0;
			return;
		}
		if (target.gameObject.name == "secondRoomCollider" && this.gameObject.name == "level1_guard1")
		{
			currentRoom = "level1middleRoom";
			oldArray = wayPointsArray;
			wayPointsArray = lc.level1middleArrayDown;
			tempPos = 0;
			return;
		}
		if (target.gameObject.name == "secondRoomCollider" && this.gameObject.name == "level1_guard3")
		{
			currentRoom = "level1middleRoom";
			oldArray = wayPointsArray;
			wayPointsArray = lc.level1middleArrayUp;
			tempPos = 0;
			return;
		}
		if (target.gameObject.name == "thirdRoomCollider" && this.gameObject.name != "level1_guard3")
		{
			currentRoom = "level1topRoom";
			oldArray = wayPointsArray;
			wayPointsArray = lc.level1topLeftArray;
			tempPos = 0;
			return;
		}

		switch (target.gameObject.name)
		{
		//level2
		case "level2bottomLeft":
			currentRoom = "level2bottomLeft";
			oldArray = wayPointsArray;
			wayPointsArray = lc.level2bottomLeftArray;
			tempPos = 0;
			break;
		case "level2bottomRight":
			currentRoom = "level2bottomRight";
			oldArray = wayPointsArray;
			wayPointsArray = lc.level2bottomRightArray;
			tempPos = 0;
			break;
		case "level2right":
			currentRoom = "level2right";
			oldArray = wayPointsArray;
			wayPointsArray = lc.level2rightArray;
			tempPos = 0;
			break;
		case "level2topRight":
			currentRoom = "level2topRight";
			oldArray = wayPointsArray;
			wayPointsArray = lc.level2topRightArray;
			tempPos = 0;
			break;
		case "level2top":
			currentRoom = "level2top";
			oldArray = wayPointsArray;
			wayPointsArray = lc.level2topArray;
			tempPos = 0;
			break;
		case "level2topLeft":
			currentRoom = "level2topLeft";
			oldArray = wayPointsArray;
			wayPointsArray = lc.level2topLeftArray;
			tempPos = 0;
			break;
		case "level2left":
			currentRoom = "level2left";
			oldArray = wayPointsArray;
			wayPointsArray = lc.level2leftArray;
			tempPos = 0;
			break;
		case "level2middle":
			currentRoom = "level2middle";
			oldArray = wayPointsArray;
			wayPointsArray = lc.level2middleArray;
			tempPos = 0;
			break;

		}


    }//End OnTriggerEnter2D

    public void returnToClosestWaypoint()
    {
        //Debug.Log("returnToClosestWaypoint: " + this.gameObject.name);
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

}
