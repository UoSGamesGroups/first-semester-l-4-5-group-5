using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

    [Header("Waypoint Arrays")]
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
