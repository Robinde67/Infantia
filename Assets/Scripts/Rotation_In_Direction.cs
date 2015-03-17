using UnityEngine;
using System.Collections;

public class Rotation_In_Direction : MonoBehaviour {

	private Vector3 last_position;

	// Use this for initialization
	void Start () {
		last_position = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 gopos = gameObject.transform.position;
		if(last_position.x != gopos.x && last_position.y != gopos.y)
		{
			Vector3 newPos = (gopos - last_position);
			float angle = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

			//Vector3 newPos = (gopos - last_position) + gopos;
			//transform.LookAt(newPos);

			//Vector3 relativePos = (transform.position - last_position);
			//transform.rotation = Quaternion.LookRotation(relativePos);
		}
		last_position = gameObject.transform.position;
	}
}
