using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHighScore : MonoBehaviour {

    public Text hiScoreText;

	// Use this for initialization
	void Start () {
        int hiscore = PlayerPrefs.GetInt("HiScore", 0);
        hiScoreText.text = hiscore.ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
