using UnityEngine;
using System.Collections;

public class Grid_Lock : MonoBehaviour {

	public Vector3 gridSize = new Vector3(1, 1, 1);

	// Use this for initialization
	void Start () {
		Adjust ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Adjust()
	{
		Vector3 newPos = transform.position;

		transform.position = new Vector3(Mathf.Round(newPos.x / gridSize.x) * gridSize.x,
		                                 Mathf.Round(newPos.y / gridSize.y) * gridSize.y,
		                                 Mathf.Round(newPos.z / gridSize.z) * gridSize.z);

		/*
		transform.position = new Vector3(Mathf.Round(newPos.x / gridSize.x) * gridSize.x,
		                     transform.position.y,
		                     Mathf.Round(newPos.z / gridSize.z) * gridSize.z);
		                     */
	}
}
