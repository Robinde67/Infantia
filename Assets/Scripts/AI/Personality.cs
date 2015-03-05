using UnityEngine;
using System.Collections;

public class Personality : MonoBehaviour {

    public short variaty = 10;

	public short proactive;
	public short social;
	public short intelligence;

	// Use this for initialization
	void Start ()
    {
        variaty = (short)Mathf.Abs(variaty);
        proactive = (short)Random.Range(-variaty, variaty);
        social = (short)Random.Range(-variaty, variaty);
        intelligence = (short)Random.Range(-variaty, variaty);
	}
	

}
