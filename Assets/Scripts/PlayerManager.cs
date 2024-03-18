using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public static bool levelStarted = false;
    public static bool gameOver;
    public GameObject startMenuPanel;
    public GameObject gamerOverPanel;
    public GameObject highScore;
    public GameObject quitGamePanel;
   

    public static int Gems;
    public TextMeshProUGUI gemsText;

    public static int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {

        gameOver = levelStarted = false;
        Time.timeScale = 1;
        Gems = 0;
        score = 0;

    }

    // Update is called once per frame
    void Update()
    {
        gemsText.text = (PlayerPrefs.GetInt("TotalGems", 0) + Gems).ToString();
        //gemsText.text = (PlayerPrefs.GetInt("TotalGems")).ToString();
        scoreText.text = "Score : " + score.ToString();
        Touchscreen ts = Touchscreen.current;
        if (ts != null && ts.primaryTouch.press.isPressed && !levelStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            levelStarted = true;
            startMenuPanel.SetActive(false);
        }

        BackButtonClicked();
        if (gameOver)
        {
            PlayerPrefs.SetInt("TotalGems", PlayerPrefs.GetInt("TotalGems", 0) + Gems);
            Time.timeScale = 0;
            gamerOverPanel.SetActive(true);

            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                highScoreText.text = "New HighScore: " + score.ToString();
                highScore.SetActive(true);
                PlayerPrefs.SetInt("HighScore", score);
            }

            this.enabled = false;
        }
    }

    public void BackButtonClicked()
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Time.timeScale = 0;
                quitGamePanel.SetActive(true);
            }
        }
        else
        {


            //Debug.Log("Pressed in Editor");
        }

    }

    public void YesButton()
    {
        Application.Quit();
    }
    public void NoButton()
    {
        Time.timeScale = 1;
        PlayerController.Instance.rotationSpeed = 0.3f;
        quitGamePanel.SetActive(false);

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=8094495406725672456&hl=en_IN&gl=US");
    }

    public void RateUs()
    {
        Application.OpenURL("market://details?id=" + Application.productName);
    }
}
