using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

	[System.Serializable]
	public struct Effects
	{
		public string name;
		public short boredom;
	}
	
	public Effects m_effects;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{

	}
	
	public Effects Interact()
	{
		if (GetComponent<SpriteRenderer>().enabled)
		{
			return m_effects;
		}
		else
		{
			return new Effects();
		}
	}
	//-----------------------------------------------------------------
	//random range, so you get a general idea by watching it, you will most likely see if something is very deadly (but you could also fail and see it as healthy).
	//memory will take precedence over perception once it have gained a memory of it.
	//-----------------------------------------------------------------
	public Effects Sense()
	{
		Effects percieve;
		percieve.name = gameObject.name;
		percieve.boredom = (short)Random.Range(0, m_effects.boredom);
		return percieve;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		other.SendMessage("OnFoundInteractable", gameObject, SendMessageOptions.DontRequireReceiver);
		
	}
}
