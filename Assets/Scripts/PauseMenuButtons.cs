using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour {

    public PauseManager pauseManager;

	// Use this for initialization
	void Start () {
        pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ResumeButton()
    {
        pauseManager.disableCanvas();
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("99_TestLevel", LoadSceneMode.Single);
    }
}
