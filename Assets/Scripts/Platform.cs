using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public Material defaultMaterial;
    public Material highlightMaterial;

	public GameObject orbObject;

	public float orbSpawnChance;

	private Vector3 orbSpawnLoc;

    public Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
		orbSpawnLoc = transform.GetChild (0).transform.position;

		float orbChance = Random.Range(0f,1f);

		if (orbChance <= orbSpawnChance)
			spawnOrb ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void spawnOrb()
	{
		Instantiate (orbObject, orbSpawnLoc, Quaternion.identity);
	}
}
