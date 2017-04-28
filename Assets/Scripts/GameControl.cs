using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public static GameControl singleton;

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
        Invoke("StartTurn", 5);

        if(isFirstTurn)
        {
            isFirstTurn = false;
            return;
        }

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

        players[turn].myJackpot.gameObject.SetActive(true);
    }
}
