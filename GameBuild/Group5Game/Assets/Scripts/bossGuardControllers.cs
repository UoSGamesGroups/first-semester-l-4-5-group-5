using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossGuardControllers : MonoBehaviour
{
    GameObject player;
    PlayerController pc;

    float guardScale;

    public GameObject[] wayPointsArray;

    Rigidbody2D rb;
    float moveSpeed = 2f;
    public int takeOverTime;

    private int currPos;

    public int currentState;

    bool canAttack;

    Animator animCon;

    public GameObject childTrigger;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        guardScale = 4;
        currPos = 0;
        currentState = 1;
        canAttack = true;

        animCon = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.controlObject != this.gameObject)
        {
            //Move
            if (currentState == 1)
                MoveToWayponts();
            //Stand
            else if (currentState == 2)
            { /* no op */ }
        }
        if (pc.controlObject == this.gameObject)
        {
            if (Input.GetKey(pc.k_operate) && canAttack)
            {
                Attack();
            }
        }

    }



    void MoveToWayponts()
    {
        Vector2 movePos = new Vector2(wayPointsArray[currPos].transform.position.x - transform.position.x, wayPointsArray[currPos].transform.position.y - transform.position.y);
        rb.velocity = movePos.normalized * moveSpeed;

        //Guard facing left or right
        if (movePos.x < 0)
            transform.localScale = new Vector2(-guardScale, guardScale);
        else
            transform.localScale = new Vector2(guardScale, guardScale);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        //--------------------------
        //Walking into waypoint

        if (target.gameObject == wayPointsArray[0])
        {
            currentState = 2;
            rb.velocity = new Vector2(0, 0);
        }
    }

    void Attack()
    {
        animCon.SetTrigger("Attack");
        StartCoroutine(attackWait(1.0f));
    }

    IEnumerator attackWait(float waitTime)
    {
        childTrigger.SetActive(true);
        canAttack = false;
        yield return new WaitForSeconds(waitTime);
        childTrigger.SetActive(false);
        canAttack = true;
    }


}