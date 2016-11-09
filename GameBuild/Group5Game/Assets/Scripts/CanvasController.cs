using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    [Header("TopLeft Timer Objects")]
    public GameObject controlBackground;
    public GameObject controlText;

    public Scrollbar timerScrollBar;
    public float timerScrollSize;

    [Header("TopRight Charge Objects")]
    public GameObject chargeBackground;
    public GameObject chargeText;
    public GameObject chargeSprite1;
    public GameObject chargeSprite2;
    public GameObject chargeSprite3;
    public GameObject chargeSprite4;

    [Header("Bottom Diary Objects")]
    public GameObject diaryBar;
    public GameObject diaryBarIcon1;
    public GameObject diaryBarIcon2;
    public GameObject diaryBarIcon3;
    public GameObject diaryBarIcon4;
    public GameObject diaryLarge1;
    public GameObject diaryLarge2;
    public GameObject diaryLarge3;
    public GameObject diaryLarge4;

    [Header("Sprites")]
    public Sprite activeCharge;
    public Sprite inactiveCharge;

    [Header("Player")]
    public GameObject player;
    PlayerController pc;

    // Use this for initialization
    void Start ()
    {
        pc = player.GetComponent<PlayerController>();
	}

    public void updateCharges()
    {
        chargeSprite1.GetComponent<Image>().sprite = inactiveCharge;
        chargeSprite2.GetComponent<Image>().sprite = inactiveCharge;
        chargeSprite3.GetComponent<Image>().sprite = inactiveCharge;
        chargeSprite4.GetComponent<Image>().sprite = inactiveCharge;

        if (pc.getCharges() > 0)
            chargeSprite1.GetComponent<Image>().sprite = activeCharge;
        if (pc.getCharges() > 1)
            chargeSprite2.GetComponent<Image>().sprite = activeCharge;
        if (pc.getCharges() > 2)
            chargeSprite3.GetComponent<Image>().sprite = activeCharge;
        if (pc.getCharges() > 3)
            chargeSprite4.GetComponent<Image>().sprite = activeCharge;
    }

    public void updateTakeoverBar()
    {
        timerScrollBar.size = ( timerScrollSize / pc.getTakeoverTimer() );
    }

}
