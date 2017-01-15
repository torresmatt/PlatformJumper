using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
    // location of camera (passed in from editor)
    public Transform cam;

    // used for initializing AND tracking the camera distance
    public float camDistance;

    // used to keep track of camera distance for later when zooming in to first person
    private float lastDistance;

    // used to track whether we are "zoomed" in to first person mode
    private bool zoomed;

    // whether or not we are allowed to move the camera
    public bool movementAllowed;

    // used to clamp the allowable distance for the camera (only when scrolling)
    public float minDistance;
    public float maxDistance;

    // speed at which distance is changed when scroll wheel is used ONLY
    public float distChangeSpeed;

    // speed at which camera rig is rotated
    public float rotationSpeed;

    // starting x axis angle for camera
    public float startAngle;

    // used to track what rotations should be (before applied directly to transform)
    private float xRotation,yRotation;

	// Use this for initialization
	void Start () 
    {
        // set current y rotation to starting angle
        yRotation = startAngle;

        // set camera distance to starting distance
        cam.localPosition = new Vector3(0, 0, -camDistance);

        // store the current distance in the lastdistance variable for later use
        lastDistance = camDistance;

        // not zoomed at start
        zoomed = false;

        // can move at start of scene
        movementAllowed = true;

        StartCoroutine(lerpToDist(0));
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!movementAllowed)
            return;

        // if F is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // create a variable to set the target distance depending on state of zoom
            float targetDistance;

            // if we are not zoomed in
            if (!zoomed)
            {
                // set target distance to 0 since that is what we want to zoom to (eye level)
                targetDistance = 0;
                // since we are zooming, flag us as zoomed
                zoomed = true;
            }
            // if we ARE zoomed already
            else
            {
                // the target distance should be the last known non-zoomed distance
                targetDistance = lastDistance;
                // mark us as not zoomed in anymore
                zoomed = false;
            }

            // use coroutine to lerp to the desired distance
            StartCoroutine(lerpToDist(targetDistance));

        }
	}

    void LateUpdate()
    {
        if (!movementAllowed)
            return;

        // store what scroll wheel is doing
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        // if doing something, do some stuff
        if (scrollWheel != 0)
        {
            lastDistance = camDistance;
            if (scrollWheel < 0)
                zoomed = false;
            // update the camera distance variable to reflect scrollwheel action
            camDistance -= scrollWheel * distChangeSpeed;
            // Make sure the distance doesn't go too close (clamp it)
            camDistance = Mathf.Clamp(camDistance,minDistance,maxDistance);
        }


        // I borrowed this bit of code from: http://answers.unity3d.com/questions/172728/transfromrotate-changes-z-axis-incorrectly.html

        // apply rotation from Y axis of mouse and clamp it to within 90 degrees
        yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;
        yRotation = Mathf.Clamp(yRotation, -90, 90);


        // apply rotation from X axis of mouse and eliminate any rotations of 360 degrees using modulo
        xRotation += Input.GetAxis("Mouse X") * rotationSpeed;
        xRotation = xRotation % 360;

        transform.localEulerAngles = new Vector3(yRotation, 0, 0);
        transform.parent.eulerAngles = new Vector3(0, xRotation, 0);

        // update camera z position based on distance
        cam.localPosition = new Vector3(0,0,-camDistance);
    }

    IEnumerator lerpToDist(float target)
    {
        // if the camera distance is already at target, do nothing
        if (camDistance == target)
        {
            yield break;
        }

        // variable to control rate of lerping
        float rate = .01f;

        // if we are "behind" the target distance
        if (camDistance > target)
        {
            // start looping until we get within .1 of target
            while (camDistance >= target + .1)
            {
                // lerp cam distance by the rate and timedelta for smoothness
                camDistance = Mathf.Lerp(camDistance, target, Time.deltaTime * rate);
                // in crease the rate (looks cool!)
                rate *= 1.5f;
                // yield control back for this tick
                yield return null;
            }
        }
        else
        {
            // this code is just a modified version of the above if statement code
            // used for the opposite scenario in which we are lerping BACK to a "larger"
            // zoom value
            while (camDistance <= target - .1)
            {
                camDistance = Mathf.Lerp(camDistance, target, Time.deltaTime * rate);
                rate *= 1.5f;
                yield return null;
            }           
        }

        // now that we are here we can stop lerping (otherwise we will lerp forever) and just
        // hard-set the distance to the target;
        camDistance = target;
    }
}
