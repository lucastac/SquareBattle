using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowPauseMenu()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void HidePauseMenu()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
