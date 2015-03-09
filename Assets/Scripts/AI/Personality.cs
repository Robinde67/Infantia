using UnityEngine;
using System.Collections;

public class Personality : MonoBehaviour {

    public short variety = 10;

	public short proactive;
	public short social;
	public short intelligence;

	// Use this for initialization
	void Start ()
    {
        variety = (short)Mathf.Abs(variety);
        proactive = (short)Random.Range(-variety, variety);
        social = (short)Random.Range(-variety, variety);
        intelligence = (short)Random.Range(-variety, variety);
	}
	

}
