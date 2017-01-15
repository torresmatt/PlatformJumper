using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUpdater : MonoBehaviour {

	public Canvas hudCanvas;
	public Text tpText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate()
	{
		tpText.text = "TELEPORTS: " + CollectibleManager.numTeleports;
	}
}
