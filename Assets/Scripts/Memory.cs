using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Memory : MonoBehaviour {

	public struct EdibleEffects
	{
		public Vector3 position;
		public short hunger,
		poison,
		pain,
		taste;
	}

	private List<EdibleEffects> memories_edible = new List<EdibleEffects>();
	AI_astar astar;

	// Use this for initialization
	void Start () {
		astar = GetComponent<AI_astar>();
		Vector3 randomPos = transform.position;
		while(Vector3.Distance(randomPos, transform.position) < 5.0f)
		{
			randomPos = new Vector3(Random.Range(0, (int)astar.grid.x), Random.Range(0, (int)astar.grid.y));
		}
		//astar.Recalculate(randomPos);
		astar.SetTarget(randomPos);
		astar.InitAStar();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnFoundEdible(EdibleEffects _memory) //Is it possible to send a struct directly in some way?
	{
		for(int i = 0; i < memories_edible.Count; i++)
		{
			if(memories_edible[i].position == _memory.position)
			{
				//Gonna need to change depending on how accurate the memory should be. Not enough time or brain-capacity to think now
				memories_edible[i] = _memory;
			}
		}
	}
}
