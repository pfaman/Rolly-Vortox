using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BallSelector : MonoBehaviour
{

    public GameObject[] ballSkins;
    private int selectedBall=0;

    public Ball[] balls;
    public Button[] buttons;
    public Button unlockButton;

    private void Awake()
    {
        int counter = 0;
        foreach( Ball b in balls)
        {
            b.index = counter;
            if(counter == 0)
            {
                b.isLocked = false;
            }
            else
            {
                if (PlayerPrefs.GetInt(b.index.ToString(), 1) == 1)
                    b.isLocked = true;
                else
                    b.isLocked = false;
                buttons[b.index].interactable = !b.isLocked;
            }
            counter++;  
        }
    }
    private void Start()
    {
        selectedBall = PlayerPrefs.GetInt("SelectedBall", 0);
        foreach(GameObject skin in ballSkins)
        {
            skin.SetActive(false);
        }
        ballSkins[selectedBall].SetActive(true);
    }

    public void ChangeBall(int index)
    {
        ballSkins[selectedBall].SetActive(false);
        selectedBall = index;
        ballSkins[selectedBall].SetActive(true);
        PlayerPrefs.SetInt("SelectedBall", index);
    }

    public void Unlock()
    {
        // Get the locked Ball
        List<Ball> lockedBalls = new List<Ball>();

        foreach(Ball b in balls)
        {
            if (b.isLocked)
                lockedBalls.Add(b);
        }
        if (lockedBalls.Count == 0)
            return;

        // Select random ball
        int randomBall = Random.Range(0, lockedBalls.Count);

        //Unlock the ball

        int ballIndex = lockedBalls[randomBall].index;
        balls[ballIndex].isLocked = false;
        buttons[ballIndex].interactable = true;
        PlayerPrefs.SetInt(ballIndex.ToString(), 0);
        PlayerPrefs.SetInt("TotalGems", PlayerPrefs.GetInt("TotalGems") - 50);


        // select ball
        buttons[ballIndex].onClick.Invoke();
       
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("TotalGems", 0) < 50)
            unlockButton.interactable = false;
        else
            unlockButton.interactable = true;
    }
}
