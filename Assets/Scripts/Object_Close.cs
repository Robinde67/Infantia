using UnityEngine;
using System.Collections;

public class Object_Close : MonoBehaviour {

	public Grid_Space grid;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("Found object with the tag " + other.tag);
		if(other.tag == "Player")
		{
			Debug.Log ("<color=yellow>Found another infant</color>");
			//Debug.Log ("Distance to " + other.name + " is " + Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(other.transform.position.x, other.transform.position.y)));
			Memory gom = gameObject.GetComponent<Memory>();
			Memory om = other.GetComponent<Memory>();

			for(int i = 0; i < om.memories_edible.Count; i++)
			{
				//int rand = Random.Range(0, om.memories_edible[i].positions.Count);
				//Debug.Log ("Memories (edible) before: " + om.memories_edible[i].positions.Count);

				for(int j = 0; j < om.memories_edible[i].positions.Count; j++)
				{
					Vector3 pos = om.memories_edible[i].positions[j];
					gom.OnFoundEdible(grid.test[(int)pos.x][(int)pos.y].gameObject);
				}

				//Debug.Log ("Memories (edible) after: " + om.memories_edible[i].positions.Count);
			}
			for(int i = 0; i < om.memories_interactables.Count; i++)
			{
				//int rand = Random.Range(0, om.memories_interactables[i].positions.Count);
				for(int j = 0; j < om.memories_interactables[i].positions.Count; j++)
				{
					Vector3 pos = om.memories_interactables[i].positions[j];
					gom.OnFoundInteractable(grid.test[(int)pos.x][(int)pos.y].gameObject);
				}
			}
		}
	}
}
