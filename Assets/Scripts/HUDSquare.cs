using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSquare : MonoBehaviour {

    public GeneralSquare targetSquare; //target square to show info

    //text reference on the HUD
    public Text levelText;
    public Text healthText;
    public Text expText;

    public Slider healthSlider;
    public Slider expSlider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (targetSquare == null) return;

        if (levelText != null) levelText.text = targetSquare.level.ToString();
        if (healthText != null) healthText.text = targetSquare.maxHealth + " / " + targetSquare.health;
        if (expText != null) expText.text = targetSquare.level < 3 ? targetSquare.experience + " / " + targetSquare.level * 2 : "0 / 0";

        if(healthSlider != null)
        {
            healthSlider.maxValue = targetSquare.maxHealth;
            healthSlider.value = targetSquare.health;
        }

        if (expSlider != null)
        {
            expSlider.maxValue = targetSquare.level < 3 ? targetSquare.maxHealth : 1;
            expSlider.value = targetSquare.level < 3 ? targetSquare.health : 1;
        }
    }
}
