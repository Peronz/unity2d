using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance;	

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countdownPage;
    public Text scoreText;

    private float heldTime = 0.0f;
    public int savedScore = 0;


    enum PageState{
	None,
	Start,
	GameOver,
	Countdown
    }

    int score = 0;
    bool gameOver = true;

    public bool GameOver { get; private set; } = true;
    public int Score { get { return score; }}
	
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    void OnEnable() {
	    CountdownText.OnCountdownFinished += OnCountdownFinished;
	    TapController.OnPlayerDied += OnPlayerDied;
	    TapController.OnPlayerScored += OnPlayerScored;
    }
    void OnDisable () {
	    CountdownText.OnCountdownFinished -= OnCountdownFinished;
	    TapController.OnPlayerDied -= OnPlayerDied;
	    TapController.OnPlayerScored -= OnPlayerScored;
    }
    void OnCountdownFinished() {
	SetPageState(PageState.None);
	OnGameStarted();
	score = 0;
	GameOver = false;
    }

    void OnPlayerDied() {
	    GameOver = true;
        //int savedScore = 0;
        if( score > 200)
            {
                savedScore += 1;
            } 
	    //int savedScore = PlayerPrefs.GetInt("HighScore");
	    //if (score > savedScore && score > 200) {
		    //PlayerPrefs.SetInt("HighScore", score);
        //}
	    SetPageState(PageState.GameOver);
    }
    void OnPlayerScored() {
        //score++;
        //scoreText.text = score.ToString();
        if (GameOver == false)
        {
            heldTime += Time.deltaTime;
            if (heldTime >= 1)
            {
                score += (int)heldTime;
                heldTime -= (int)heldTime;
                scoreText.text = score.ToString();
            }
        }

    }	
    // Update is called once per frame
    void SetPageState(PageState state) {
	switch(state) {
		case PageState.None:
			startPage.SetActive(false);
			gameOverPage.SetActive(false);
			countdownPage.SetActive(false);

			break;
		case PageState.Start:
			startPage.SetActive(true);
			gameOverPage.SetActive(false);
			countdownPage.SetActive(false);

			break;
		case PageState.GameOver:
			startPage.SetActive(false);
			gameOverPage.SetActive(true);
			countdownPage.SetActive(false);

			break;
		case PageState.Countdown:
			startPage.SetActive(false);
			gameOverPage.SetActive(false);
			countdownPage.SetActive(true);

			break;
        }
    }

    public void ConfirmGameOver() {

	OnGameOverConfirmed();
	scoreText.text = "0";
	SetPageState(PageState.Start);

    }

    public void StartGame() {

	SetPageState(PageState.Countdown);

    }
    
        
    
}
