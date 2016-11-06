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

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerController>();
		rb = GetComponent<Rigidbody2D>();

		currPos = 0;
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

        //Guard facing left or right
        if (movePos.x < 0)
            transform.localScale = new Vector2(-pc.level1_guard1_xScale, pc.level1_guard1_yScale);
        else
            transform.localScale = new Vector2(pc.level1_guard1_xScale, pc.level1_guard1_yScale);
    }

    void OnTriggerEnter2D(Collider2D target)
    {

		for (int i = 0; i < wayPointsArray.Length; i++)
		{
			if (target.gameObject == wayPointsArray[i] && target.gameObject != wayPointsArray[wayPointsArray.Length-1])
			{
				currPos++;
				return;
			}
		}

		if (target.gameObject == wayPointsArray[wayPointsArray.Length -1] && gameObject.tag != "level2Guards")
		{
			currPos = 0;
			return;
		}
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
