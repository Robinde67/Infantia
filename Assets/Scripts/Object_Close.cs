using UnityEngine;
using System.Collections;

public class Object_Close : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("Found object with the tag " + other.tag);
		if(other.tag == "World Object")
		{
			//Debug.Log ("Distance to " + other.name + " is " + Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(other.transform.position.x, other.transform.position.y)));


		}
	}
}
