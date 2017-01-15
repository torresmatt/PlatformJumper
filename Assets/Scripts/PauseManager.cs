using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public Canvas pauseCanvas;

    private PlayerMovement playerMovement;
    private CameraController cameraController;

	// Use this for initialization
	void Start () 
    {
        pauseCanvas.enabled = false;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        cameraController = GameObject.Find("CameraBoom").GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggle();
        }
	}

    public void toggle()
    {
        if (pauseCanvas.enabled)
            disableCanvas();
        else
            enableCanvas();
    }

    public void enableCanvas()
    {
        pauseCanvas.enabled = true;
        Time.timeScale = 0;
        playerMovement.inputAllowed = false;
        cameraController.movementAllowed = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void disableCanvas()
    {
        pauseCanvas.enabled = false;
        Time.timeScale = 1;
        playerMovement.inputAllowed = true;
        cameraController.movementAllowed = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
