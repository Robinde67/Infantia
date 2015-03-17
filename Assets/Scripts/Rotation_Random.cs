using UnityEngine;
using System.Collections;

public class Rotation_Random : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		gameObject.transform.eulerAngles.Set(transform.eulerAngles.x, transform.eulerAngles.y, Random.Range(0.0f, 360.0f));
	}
}
