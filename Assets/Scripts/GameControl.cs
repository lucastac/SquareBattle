using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public static GameControl singleton;

    public GameObject endGameWindow;
    public Player[] players;
    public int turn = 0;
    private bool isFirstTurn = true; //on the end of first turn, no one attacks
	// Use this for initialization
	void Start () {
        singleton = this;

        for (int i = 1; i < players.Length; i++)
            players[i].myJackpot.gameObject.SetActive(false);

        players[0].myJackpot.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndTurn()
    {
        

        turn = (turn + 1) % players.Length;
        

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
    }

    private void DeclareWinner(int winnerID)
    {
        endGameWindow.SetActive(true);
        Text winText = endGameWindow.GetComponentInChildren<Text>();
        if(winnerID == 0)
        {
            winText.text = "Blue Win";            
        }
        else
        {
            winText.text = "Red Win";
        }

        winText.color = players[winnerID].MainSquare.GetComponentInChildren<SpriteRenderer>().color;
    }
}
