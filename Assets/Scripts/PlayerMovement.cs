using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float jumpHeight;

    public Collider feet;

    private Transform cameraBoom;

    private bool grounded;
    public bool inputAllowed;

    private Rigidbody rb;

    private float sprintSpeed,normalSpeed;

    private Platform platformLastHit;
    private Platform activePlatform;

    private Lerper lerper;

	// Use this for initialization
	void Start () 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        grounded = false;
        inputAllowed = true;
        sprintSpeed = speed * 1.5f;
        normalSpeed = speed;
        cameraBoom = transform.GetChild(0).transform;
        activePlatform = null;
        lerper = GameObject.Find("Lerper").GetComponent<Lerper>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!inputAllowed)
            return;
        
        float vert = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = normalSpeed;
        }

        if (vert != 0 || horiz != 0)
        {
            float airControlScale;

            if (grounded)
            {
                airControlScale = 1f;
            }
            else
            {
                airControlScale = .2f;
            }

            if (vert != 0)
            {
                transform.Translate(Vector3.forward * vert * speed * Time.deltaTime * airControlScale);
            }

            if (horiz != 0)
            {
                transform.Translate(Vector3.right * horiz * speed * Time.deltaTime * airControlScale);
            }
        }
            
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddRelativeForce(new Vector3(horiz * speed * .66f, jumpHeight, vert * speed * .66f),ForceMode.Impulse);
            grounded = false;
        }

        // teleportation code here
	}

    void FixedUpdate()
    {
        if (!inputAllowed)
            return;

        if (!Input.GetKey(KeyCode.Mouse1))
        {
            if (platformLastHit != null)
                platformLastHit.renderer.material = platformLastHit.defaultMaterial;
            return;
        }
            
        RaycastHit hit;
        if (Physics.Raycast(cameraBoom.position,cameraBoom.forward, out hit, 3f))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                Platform platform = hit.collider.gameObject.GetComponent<Platform>();
                platform.renderer.material = platform.highlightMaterial;
                platformLastHit = platform;
                activePlatform = platform;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    telePort();
            }
        }
        else
        {
            activePlatform = null;
            if (platformLastHit != null)
                platformLastHit.renderer.material = platformLastHit.defaultMaterial;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    void telePort()
    {
		if (activePlatform == null || CollectibleManager.numTeleports == 0)
            return;
		CollectibleManager.numTeleports--;
        StartCoroutine(lerper.Lerp(activePlatform.transform.GetChild(0).transform, transform));
    }
}
