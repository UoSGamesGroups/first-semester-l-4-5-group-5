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
    public GameObject diaryLarge;

    [Header("Sprites")]
    public Sprite activeCharge;
    public Sprite inactiveCharge;
    public Sprite barBlackIcon;
    public Sprite barDiaryIcon;
    public Sprite diaryEntry1;
    public Sprite diaryEntry2;
    public Sprite diaryEntry3;
    public Sprite diaryEntry4;

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

    public void updateInventory()
    {
        if (pc.hasDiary1)
            diaryBarIcon1.GetComponent<Image>().sprite = barDiaryIcon;
        else
            diaryBarIcon1.GetComponent<Image>().sprite = barBlackIcon;

        if (pc.hasDiary2)
            diaryBarIcon2.GetComponent<Image>().sprite = barDiaryIcon;
        else
            diaryBarIcon2.GetComponent<Image>().sprite = barBlackIcon;

        if (pc.hasDiary3)
            diaryBarIcon3.GetComponent<Image>().sprite = barDiaryIcon;
        else
            diaryBarIcon3.GetComponent<Image>().sprite = barBlackIcon;

        if (pc.hasDiary4)
            diaryBarIcon4.GetComponent<Image>().sprite = barDiaryIcon;
        else
            diaryBarIcon4.GetComponent<Image>().sprite = barBlackIcon;
    }

    public void displayDiary1()
    {
        if (pc.hasDiary1)
        {
            diaryLarge.SetActive(true);
            diaryLarge.GetComponent<Image>().sprite = diaryEntry1;
        }
    }

    public void displayDiary2()
    {
        if (pc.hasDiary2)
        {
            diaryLarge.SetActive(true);
            diaryLarge.GetComponent<Image>().sprite = diaryEntry2;
        }
    }

    public void displayDiary3()
    {
        if (pc.hasDiary3)
        {
            diaryLarge.SetActive(true);
            diaryLarge.GetComponent<Image>().sprite = diaryEntry3;
        }
    }

    public void displayDiary4()
    {
        if (pc.hasDiary4)
        {
            diaryLarge.SetActive(true);
            diaryLarge.GetComponent<Image>().sprite = diaryEntry4;
        }
    }

    public void hideDiary()
    {
        diaryLarge.SetActive(false);
    }


    //I initially tried to use this function to controll the mouse-over for the "inventory bar",
    //however the event-trigger system only seems to let me call functions that do not take 
    //in any arguments, and so would not let me call this method. 
    public void diaryDisplay(bool display, int diary)
    { 
        if (display)
        {
            if (diary == 1 && pc.hasDiary1)
            {
                diaryLarge.SetActive(true);
                diaryLarge.GetComponent<Image>().sprite = diaryEntry1;
            }
            else if (diary == 2 && pc.hasDiary2)
            {
                diaryLarge.SetActive(true);
                diaryLarge.GetComponent<Image>().sprite = diaryEntry2;
            }
        }
        else
        {
            diaryLarge.SetActive(false);
        }
    }

}
