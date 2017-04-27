using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public List<GeneralSquare> mySquares;
    public GeneralSquare MainSquare;
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
}
