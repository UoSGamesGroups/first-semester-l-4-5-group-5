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
		//Movement
		if (target.gameObject == wayPointsArray[0]|| target.gameObject == wayPointsArray[1] || target.gameObject == wayPointsArray[2])
        {
            currPos++;
        }
        else
            currPos = 0;


		//Player related

		//Obtain level1_key1
		if (pc.controlObject == this.gameObject)
		{
			if (this.gameObject.name == "level1_guard1" && target.name == "level1_key1")
			{
				Destroy (target.gameObject);
                Debug.Log("level1_guard1 collected level1_key1");
				pc.hasLevel1_key1 = true;
			}
		}

		//Obtain level1_key2
		if (pc.controlObject == this.gameObject)
		{
			if (this.gameObject.name == "level1_guard2" && target.name == "level1_key2")
			{
				Destroy(target.gameObject);
				Debug.Log("level1_guard2 collected level1_key2");
				pc.hasLevel1_key2 = true;
			}
		}

    }//End OnTriggerEnter2D

}
