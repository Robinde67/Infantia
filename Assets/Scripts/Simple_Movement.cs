using UnityEngine;
using System.Collections;

public class Simple_Movement : MonoBehaviour {

	public GameObject target;
	public float movSpeed = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			iTween.MoveTo(gameObject, iTween.Hash("x", target.transform.position.x, "easeType", "easeOutCubic", "speed", movSpeed * Time.deltaTime * 100.0f,
			                                      "onComplete", "NextOne"));
		}
	}

	void NextOne()
	{
		iTween.MoveTo(gameObject, iTween.Hash("y", target.transform.position.y, "easeType", "easeOutCubic", "speed", movSpeed * Time.deltaTime * 100.0f));
	}
}
