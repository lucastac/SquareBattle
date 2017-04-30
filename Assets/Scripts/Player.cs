using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour {

    public List<GeneralSquare> mySquares;
    public GeneralSquare MainSquare;
    public GeneralSquare Barrier;
    public Player enemyPlayer;
    public JackPot myJackpot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool HasBarrier()
    {
        return Barrier.IsAlive();
    }

    public bool HasCombatSquare()
    {
        foreach(GeneralSquare square in mySquares)
        {
            if (square.IsAlive())
                return true;
        }

        return false;
    }

    public GeneralSquare GiveRandomTarget(bool IncludeBarrier = true)
    {
        GeneralSquare target;
        int i;
        if (HasBarrier() && IncludeBarrier)
            target = Barrier;
        else if (HasCombatSquare())
        {
            do
            {
                i = Random.Range(0, enemyPlayer.mySquares.Count);
            } while (!mySquares[i].IsAlive());
            target = mySquares[i];
        }
        else
            target = MainSquare;

        return target;
    }

    public void ProcessJackpotResult(int[] result, int type)
    {
        switch(type)
        {
            case 0:
                ProcessUnityJackpot(result);
                break;

            case 1:
                ProcessUtilityJackpot(result);
                break;

            case 2:
                ProcessOffensiveJackpot(result);
                break;
        }
    }

    

    

}
