using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Status_Bar : MonoBehaviour {
    public enum Type
    {
        HUNGER,
        SLEEPY,
        BOREDOM
    }
    public Type type;

    private Slider sl;

    public Human_Needs infant;
	// Use this for initialization
	void Start ()
    {
        sl = GetComponentInParent<Slider>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    switch (type)
        {
            case Type.HUNGER:
                sl.value = infant.hunger;
                break;
            case Type.SLEEPY:
                sl.value = infant.sleepy;
                break;
            case Type.BOREDOM:
                sl.value = infant.boredom;
                break;
        }
	}
}
