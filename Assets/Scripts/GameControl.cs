using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public static GameControl singleton;
    public static int actualGameType;//1 = PvP, 2 = PvAI

    public GameObject endGameWindow;
    public Text winText;
    public Text scoreText;
    public GameObject newHiScoreWindow;
    public GameObject printScreenButton;
    public GameObject backButton;
    int qtdTurns = 0;
    int score = 0;
    int actualShowingScore = 0;
    bool showingScore = false;

    public Player[] players;
    public static int turn = 0;
    private bool isFirstTurn = true; //on the end of first turn, no one attacks
	// Use this for initialization
	void Start () {
        singleton = this;
        turn = 0;
        actualGameType = PlayerPrefs.GetInt("gameMode", 1);
        for (int i = 1; i < players.Length; i++)
            players[i].myJackpot.gameObject.SetActive(false);

        players[0].myJackpot.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if(showingScore)
        {
            int delta = score - actualShowingScore;
            if (delta > 0)
            {
                actualShowingScore += Mathf.CeilToInt(delta * 0.05f);
                scoreText.text = actualShowingScore.ToString();
            }
            else
            {
                scoreText.text = score.ToString();
                showingScore = false;

                int hiScore = PlayerPrefs.GetInt("HiScore", 0);
                
                if (score > hiScore)
                {
                    newHiScoreWindow.SetActive(true);
                    PlayerPrefs.SetInt("HiScore", score);
                }
            }
        }
	}

    public void EndTurn()
    {
        

        turn = (turn + 1) % players.Length;
        qtdTurns++;
        

        if(isFirstTurn)
        {
            isFirstTurn = false;
            Invoke("StartTurn", 1);
            return;
        }else
            Invoke("StartTurn", 5);

        foreach (Player p in players)
        {
            foreach(GeneralSquare gs in p.mySquares)
            {
                if (gs.gameObject.activeInHierarchy)
                    gs.Act();
            }
        }

        
    }

    private void StartTurn()
    {
        for (int i = 0; i < players.Length; i++)
            players[i].myJackpot.gameObject.SetActive(false);


        for(int i = 0; i < players.Length; i++)
        {
            if(!players[i].MainSquare.gameObject.activeInHierarchy)
            {
                DeclareWinner((i + 1) % players.Length);
                return;
            }
        }

        players[turn].myJackpot.gameObject.SetActive(true);

        foreach(Player p in players)
        {
            foreach(GeneralSquare gs in p.mySquares)
            {
                gs.SetDisabled(false);
                gs.bonusAtk = 0;
            }
        }

        if(actualGameType == 2 && turn == 1)
        {
            Artificial_Intelligence.singleton.YourTurn();
        }
    }

    private void DeclareWinner(int winnerID)
    {
        endGameWindow.SetActive(true);

        if(winnerID == 0)
        {
            winText.text = "Blue Win";            
        }
        else
        {
            winText.text = "Red Win";
        }

        winText.color = players[winnerID].MainSquare.GetComponentInChildren<SpriteRenderer>().color;


        if (winnerID == 1 && actualGameType == 2)
            score = 0;
        else
            score = (players[winnerID].MainSquare.health * 250) / qtdTurns;

        showingScore = true;

        
    }

    public void PrintHiScore()
    {
        printScreenButton.SetActive(false);
        backButton.SetActive(false);
        Application.CaptureScreenshot("NewHighScore.png");
        Invoke("enableBackButton", 0.2f);

    }

    private void enableBackButton()
    {
        backButton.SetActive(true);
    }


}
