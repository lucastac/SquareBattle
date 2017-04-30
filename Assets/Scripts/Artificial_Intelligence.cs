using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artificial_Intelligence : MonoBehaviour {

    public static Artificial_Intelligence singleton;
    public Player myPlayer;
    public JackPot myJackPot;

	// Use this for initialization
	void Start () {
        singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void YourTurn()
    {
        if(myJackPot.iconsPerJackpot.Count <  3)
        {
            Invoke("YourTurn", 0.5f);
            return;
        }

        int jackpotType = SimpleTreeDecision();


        myJackPot.ChangeType(jackpotType);
        myJackPot.Roll();
        Invoke("StopRoll", 1);
    }

    private void StopRoll()
    {
        myJackPot.Roll();
    }

    //simple tree decision IA to pick a jackpot type
    private int SimpleTreeDecision()
    {
        if (totalHealthSquares() < 10) //if with low squares, then pick more squares
            return 0;

        if (totalEnemyLevel() > 3) //if enemy army to strong, then call utilities
            return 1;

        return 2; //if i am strong and enemy week, then go to offensive

    }

    private int totalHealthSquares()
    {
        int total = 0;
        foreach (GeneralSquare gs in myPlayer.mySquares)
        {
            if (gs.IsAlive())
                total += gs.health;
        }

        return total;
    }

    private int totalEnemyLevel()
    {
        int total = 0;
        foreach (GeneralSquare gs in myPlayer.mySquares)
        {
            if (gs.IsAlive())
                total += gs.level;
        }

        return total;
    }

   

}
