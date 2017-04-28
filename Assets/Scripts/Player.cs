using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public List<GeneralSquare> mySquares;
    public GeneralSquare MainSquare;
    public Player enemyPlayer;
    public JackPot myJackpot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool HasCombatSquare()
    {
        foreach(GeneralSquare square in mySquares)
        {
            if (square.gameObject.activeInHierarchy)
                return true;
        }

        return false;
    }

    public void ProcessJackpotResult(int[] result, int type)
    {
        switch(type)
        {
            case 0: ProcessUnityJackpot(result);
                break;

            case 1:
                ProcessUtilityJackpot(result);
                break;

            case 2:
                ProcessOfensiveJackpot(result);
                break;
        }
    }

    private void ProcessUnityJackpot(int[] result)
    {
        for(int i = 0; i < result.Length; i++)
        {
            if (mySquares[result[i]].gameObject.activeInHierarchy)
                mySquares[result[i]].EarnExperience(1);
            else
            {
                mySquares[result[i]].ResetStatus();
                mySquares[result[i]].gameObject.SetActive(true);                
            }
        }
    }

    private void ProcessUtilityJackpot(int[] result)
    {

    }

    private void ProcessOfensiveJackpot(int[] result)
    {

    }

}
