using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour 
{

    public GameObject platform;
    public GameObject player;

    public int platformsToSpawn;

    public List<GameObject> platforms;

    public bool playerMoved = false;

	// Use this for initialization
	void Start () 
    {
		// start in center of world
        transform.position = Vector3.zero;
        platforms = new List<GameObject>();
        playerMoved = false;
        spawnPlatform();
	}
	
	// Update is called once per frame
	void Update () 
    {
        // check if we're done
        if (platformsToSpawn <= 0)
        {
            if (!playerMoved)
                movePlayer();
            return;
        }

        // move to new location

        // pick random axis
        int axis = Random.Range(1,3);
        // pick random direction (pos or neg)
        int posNeg = Random.Range(1, 3);

        Vector3 direction = new Vector3();

        switch (axis)
        {
            case 1:
                direction = Vector3.right;
                break;
            case 2:
                direction = Vector3.forward;
                break;
            default:
                break;                
        }

        float multiplier = 0;

        switch (posNeg)
        {
            case 1:
                multiplier = 1;
                break;
            case 2:
                multiplier = -1;
                break;
            default:
                break;
        }

        // random distance for up
        float distanceUp = Random.Range(1, 3);

        // move up and then in desired direction 1
        transform.Translate(Vector3.up * distanceUp);


        float distanceOver = Random.Range(3, 6);
        transform.Translate(direction * distanceOver * multiplier);

        // spawn platform
        spawnPlatform();

	}

    void spawnPlatform()
    {
        GameObject newPlatform = Instantiate(platform,transform.position,Quaternion.identity);
        platforms.Add(newPlatform);
        platformsToSpawn--;
    }

    void movePlayer()
    {
        GameObject firstPlatform = platforms[0];
        player.transform.position = firstPlatform.transform.GetChild(0).transform.position;
        playerMoved = true;
    }
}
