using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JackPot : MonoBehaviour {

    public Player myPlayer; //target player of the jackpot
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

    private bool rolling = false;
    
	// Use this for initialization
	void Start () {
        iconsPerJackpot.Add(iconsJackpotSlot1);
        iconsPerJackpot.Add(iconsJackpotSlot2);
        iconsPerJackpot.Add(iconsJackpotSlot3);

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
                    indexes[i] = (indexes[i] + 1) % iconsPerJackpot[0].Length;
                    jackpotSlot[i].sprite = iconsPerJackpot[0][indexes[i]];
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
            indexes[i] = Random.Range(0, iconsPerJackpot[0].Length);
            jackpotSlot[i].sprite = iconsPerJackpot[0][indexes[i]];
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
    }


}
