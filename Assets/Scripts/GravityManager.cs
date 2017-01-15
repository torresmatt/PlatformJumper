using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

    public float gravityY;

	// Use this for initialization
	void Start () {
        Physics.gravity = new Vector3(0, gravityY, 0);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setGravityY(float gravity)
    {
        Physics.gravity = new Vector3(0, gravity, 0);
    }
}
