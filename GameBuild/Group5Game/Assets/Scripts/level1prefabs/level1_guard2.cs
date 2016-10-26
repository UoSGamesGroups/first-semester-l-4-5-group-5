using UnityEngine;
using System.Collections;

public class level1_guard2 : MonoBehaviour
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

        /*waypoint1 = GameObject.Find("secondRoomTopLeft");
        waypoint2 = GameObject.Find("secondRoomTopRight");
        waypoint3 = GameObject.Find("secondRoomBottomRight");
        waypoint4 = GameObject.Find("secondRoomBottomLeft");*/

        wayPointsArray = new GameObject[] { waypoint1, waypoint2, waypoint3, waypoint4 };
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (pc.controlObject != this)
            moveToWaypoints();
    }

    void moveToWaypoints()
    {
        Vector2 movePos = new Vector2(wayPointsArray[currPos].transform.position.x - transform.position.x, wayPointsArray[currPos].transform.position.y - transform.position.y);

        rb.velocity = movePos.normalized * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D target)
    {

        if (target.name == "secondRoomTopLeft" || target.name == "secondRoomTopRight" || target.name == "secondRoomBottomRight")
        {
            //Debug.Log("guard2 Target touched: " + target.name);
            currPos++;
            //Debug.Log("New guard2 pos: " + currPos);
        }
        else
            currPos = 0;

    }

}
