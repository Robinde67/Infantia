using UnityEngine;
using System.Collections;

public class Human_Needs : MonoBehaviour {

    public float drain;

	public float hunger;
	public float sleepy;
	public float boredom;
    public Personality person;

	// Use this for initialization
	void Start () {
		hunger = 255;
		sleepy = 255;
		boredom = 255;
        person = GetComponentInParent<Personality>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Drain();
	}

    void Drain()
    {
        float real_drain = (Time.fixedDeltaTime * drain) + (Time.fixedDeltaTime * ((float)person.proactive / (float)person.variaty));
        if (real_drain <= 0)
        {
            real_drain = 0.1f * Time.fixedDeltaTime;
        }
        hunger -= real_drain;
        real_drain = (Time.fixedDeltaTime * drain) + (Time.fixedDeltaTime * ((float)person.social / (float)person.variaty));
        if (real_drain <= 0)
        {
            real_drain = 0.1f * Time.fixedDeltaTime;;
        }
        boredom -= real_drain;
        real_drain = (Time.fixedDeltaTime * drain) + (Time.fixedDeltaTime * ((float)-person.proactive / (float)person.variaty));
        if (real_drain <= 0)
        {
            real_drain = 0.1f * Time.fixedDeltaTime;;
        }
        sleepy -= real_drain;
    }
}
