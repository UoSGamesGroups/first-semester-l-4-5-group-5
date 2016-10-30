using UnityEngine;
using System.Collections;

public class level1_guard3 : MonoBehaviour {

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
    void Start()
    {
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerController>();

        wayPointsArray = new GameObject[] { waypoint1, waypoint2, waypoint3, waypoint4 };
        rb = GetComponent<Rigidbody2D>();
	}

    void Update()
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
        if (target.name == "thirdRoomTopLeft" || target.name == "thirdRoomTopRight" || target.name == "thirdRoomBottomRight")
        {
            currPos++;
        }
        else
            currPos = 0;
    }
}
