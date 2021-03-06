﻿using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    [Header("Level 1 return Arrays")]
    //////////////
    ////level1////
    //////////////
	//level1Room1
	public GameObject[] level1bottomArray;

	//level1Room2
	public GameObject[] level1middleArrayUp;
	public GameObject[] level1middleArrayDown;

	//level1Room3
	public GameObject[] level1topLeftArray;

    [Header("Level New return Arrays")]
    ////////////////
    ////levelnew////
    ////////////////
    public GameObject[] levelnewLeftTopArray;
    public GameObject[] levelnewLeftBottomArray;
    public GameObject[] levelnewTopRightArray;
    public GameObject[] levelnewTopLeftArray;
    public GameObject[] levelnewNorthRoomArray;
    public GameObject[] levelnewMainRoomArray;
    public GameObject[] levelnewSouthRoomArray;

    [Header("Level 2 return Arrays")]
    //////////////
    ////level2////
    //////////////
    //level2bottomLeft
    public GameObject[] level2bottomLeftArray;

	//level2bottomRight
	public GameObject[] level2bottomRightArray;

	//level2Right
	public GameObject[] level2rightArray;

	//level2TopRight
	public GameObject[] level2topRightArray;

	//level2top
	public GameObject[] level2topArray;

	//level2topLeft
	public GameObject[] level2topLeftArray;

	//level2left
	public GameObject[] level2leftArray;

	//level2middle
	public GameObject[] level2middleArray;

    [Header("Level 3 return Arrays")]
    //////////////
    ////level3////
    //////////////
    public GameObject[] level3NorthLeftArray;
    public GameObject[] level3NorthArray;
    public GameObject[] level3RightNorthArray;
    public GameObject[] level3RightArray;
    public GameObject[] level3RightSouthArray;
    public GameObject[] level3LeftNorthArray;
    public GameObject[] level3LeftArray;
    public GameObject[] level3LeftSouthArray;
    public GameObject[] level3LeftSouthEastArray;
    //Middle arrays
    public GameObject[] level3MiddleNorthArray;
    public GameObject[] level3MiddleLeftArray;
    public GameObject[] level3MiddleRightArray;


    [Header("Sound gameObjects")]
    public GameObject chargePickup;
    public GameObject enterGuard;
    public GameObject exitGuard;
    public GameObject diaryPickup;
    public GameObject keyPickup;
    public GameObject keyUnlockDoor;
    public GameObject leverDoorUnlock;
    public GameObject leverPull;
    public GameObject songs;

    void Start ()
    {
        songs.GetComponent<AudioSource>().Play();
    }

    public void playSound(string sound)
    {
        switch (sound)
        {
            case "chargePickup":
                chargePickup.GetComponent<AudioSource>().Play();
                break;
            case "enterGuard":
                enterGuard.GetComponent<AudioSource>().Play();
                break;
            case "exitGuard":
                exitGuard.GetComponent<AudioSource>().Play();
                break;
            case "keyPickup":
                keyPickup.GetComponent<AudioSource>().Play();
                break;
            case "diaryPickup":
                diaryPickup.GetComponent<AudioSource>().Play();
                break;
            case "keyUnlockDoor":
                keyUnlockDoor.GetComponent<AudioSource>().Play();
                break;
            case "leverDoorUnlock":
                leverDoorUnlock.GetComponent<AudioSource>().Play();
                break;
            case "leverPull":
                leverPull.GetComponent<AudioSource>().Play();
                break;
        }
    }

}
