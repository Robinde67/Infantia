using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public float drain;

	public float hunger;
	public float sleepy;
	public float boredom;

    public float poison;
    public float injury;

    public Personality person;

	// Use this for initialization
	void Start () {
		hunger = 255;
		sleepy = 255;
		boredom = 255;

        poison = 0;
        injury = 0;

        person = GetComponentInParent<Personality>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Drain();
	}

    void Drain()
    {
        float real_drain = (Time.fixedDeltaTime * drain) + (Time.fixedDeltaTime * ((float)person.proactive / (float)person.variety));
        if (real_drain <= 0)
        {
            real_drain = 0.1f * Time.fixedDeltaTime;
        }
        hunger -= real_drain;
        real_drain = (Time.fixedDeltaTime * drain) + (Time.fixedDeltaTime * ((float)person.social / (float)person.variety));
        if (real_drain <= 0)
        {
            real_drain = 0.1f * Time.fixedDeltaTime;;
        }
        boredom -= real_drain;
        real_drain = (Time.fixedDeltaTime * drain) + (Time.fixedDeltaTime * ((float)-person.proactive / (float)person.variety));
        if (real_drain <= 0)
        {
            real_drain = 0.1f * Time.fixedDeltaTime;;
        }
        sleepy -= real_drain;

        if (poison < 0)
        {
            poison = 0;
        } else if (poison > 0)
        {
            poison -= Time.fixedDeltaTime / 10;
        }
        if (injury < 0)
        {
            injury = 0;
        } else if (injury > 0)
        {
            injury -= Time.fixedDeltaTime / 60;
        }
    }
}
