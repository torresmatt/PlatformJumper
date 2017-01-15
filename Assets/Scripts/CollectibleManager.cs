using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour {

	private static int maxTeleports = 3;
	private static int minTeleports = 0;

	private static int m_numTeleports = 1;

	public static int numTeleports
	{
		get 
		{
			return m_numTeleports;
		}

		set 
		{
			if (value > maxTeleports)
				m_numTeleports = maxTeleports;
			else if (value < minTeleports)
				m_numTeleports = minTeleports;
			else
				m_numTeleports = value;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
