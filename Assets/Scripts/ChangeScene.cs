using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string scene;

	public void LoadScene(int gameMode) //gameMode == 1 -> PvP, gameMode == 2 -> PvIA
    {

        if(scene.CompareTo("Quit") == 0)
        {
            Application.Quit();
            return;
        }

        PlayerPrefs.SetInt("gameMode", gameMode);
        SceneManager.LoadScene(scene);
    }

}
