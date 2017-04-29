using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JackPot : MonoBehaviour {

    public Player myPlayer; //target player of the jackpot
    public Button buttonRoll; //reference to the roll button
    public Text buttonText; //reference to the roll button text 
    public float[] periodRolling; //time for each jackpot slot change value
    private float[] timeCount = { 0, 0, 0 }; //used for counting the time for each slot
    public Image[] jackpotSlot; //Image reference for each jackpotSlot

    // icons for each jackpot type
    public Sprite[] iconsJackpotSlot1;
    public Sprite[] iconsJackpotSlot2;
    public Sprite[] iconsJackpotSlot3;

    protected List<Sprite[]> iconsPerJackpot = new List<Sprite[]>(); //the icons for each jackpot type;
    protected int[] indexes = { 0, 0, 0 }; //Index on the jackpot for each pot
    int actualType = 0; 

    private bool rolling = false;
    
	// Use this for initialization
	void Start () {
        iconsPerJackpot.Add(iconsJackpotSlot1);
        iconsPerJackpot.Add(iconsJackpotSlot2);
        iconsPerJackpot.Add(iconsJackpotSlot3);

    }

    private void OnEnable()
    {
        if(iconsPerJackpot.Count < 3)
        {
            //if the start function did not had called, then wait and try again
            Invoke("OnEnable", 0.1f);
            return;
        }

        buttonRoll.gameObject.SetActive(true);
        for(int i = 0; i < jackpotSlot.Length; i++)
        {
            jackpotSlot[i].sprite = iconsPerJackpot[actualType][i];
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void FixedUpdate()
    {
        if(rolling)
        {
            for(int i = 0; i < timeCount.Length; i++)
            {
                timeCount[i] += Time.deltaTime;
            }
            for (int i = 0; i < jackpotSlot.Length; i++)
            {
                if (timeCount[i] >= periodRolling[i])
                {
                    indexes[i] = (indexes[i] + 1) % iconsPerJackpot[actualType].Length;
                    jackpotSlot[i].sprite = iconsPerJackpot[actualType][indexes[i]];
                    timeCount[i] = 0;
                }
            }
        }
    }

    public void Roll()
    {
        if (rolling)
            StopRolling();
        else
            StartRolling();
    }

    protected void StartRolling()
    {
        for (int i = 0; i < jackpotSlot.Length; i++)
        {
            indexes[i] = Random.Range(0, iconsPerJackpot[actualType].Length);
            jackpotSlot[i].sprite = iconsPerJackpot[actualType][indexes[i]];
        }
        rolling = true;
        buttonText.text = "Stop !";
    }

    protected void StopRolling()
    {
        rolling = false;
        for (int i = 0; i < timeCount.Length; i++)
        {
            timeCount[i] = 0;
        }
        buttonText.text = "Roll !";
        buttonRoll.gameObject.SetActive(false);
        myPlayer.ProcessJackpotResult(indexes, actualType);

        Invoke("EndPlayerTurn", 1);
    }

    private void EndPlayerTurn()
    {
        GameControl.singleton.EndTurn();        
    }

    public void ChangeType(int newType)
    {
        actualType = newType;
        for (int i = 0; i < jackpotSlot.Length; i++)
        {
            jackpotSlot[i].sprite = iconsPerJackpot[actualType][i];
        }
    }
}
