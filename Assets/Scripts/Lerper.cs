using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    // This coroutine will lerp any two points together turning off physics and collision for the mover.
    public IEnumerator Lerp(Transform target, Transform mover)
    {
        float rate = .01f;
        float dist = Vector3.Distance(mover.position, target.position);
        // turn off collider on mover
        Collider moverCol = mover.gameObject.GetComponent<Collider>();
        moverCol.enabled = false;
        // turn off gravity on rigidbody on mover
        Rigidbody moverRB = mover.gameObject.GetComponent<Rigidbody>();
        moverRB.useGravity = false;
        moverRB.velocity = Vector3.zero;


        while (dist >= .1)
        {
            mover.position = Vector3.Lerp(mover.position, target.position, rate);
            dist = Vector3.Distance(mover.position, target.position);
            rate *= 1.4f;
            yield return null;
        }
        mover.position = target.position;
        // turn on collider on mover
        moverCol.enabled = true;
        // turn gravity back on for RB
        moverRB.useGravity = true;
    }
}
