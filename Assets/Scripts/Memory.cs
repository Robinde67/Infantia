using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Memory : MonoBehaviour {

    [System.Serializable]
	public struct EdibleEffects
	{
        public string name;
        public List<Vector3> positions;
        public Edible.Effects effects;
        
        public float GetValue(){
			return (effects.hunger + effects.taste - effects.pain - effects.poison);
        }
	}

	public List<EdibleEffects> memories_edible = new List<EdibleEffects>();
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

	public void OnFoundEdible(GameObject _go) //Is it possible to send a struct directly in some way? // You need more than should be sent through a struct alone.
	{
        if (!_go.GetComponent<SpriteRenderer>().enabled)
        {
            //Can't learn about something that isn't there (someone else might've eaten up everything on that square)
            return;
        }
		for(int i = 0; i < memories_edible.Count; i++)
		{
			if(memories_edible[i].name == _go.name)
			{
				//Gonna need to change depending on how accurate the memory should be. Not enough time or brain-capacity to think now
                //*For what I assume, we should only have this trigger when in an adjacent square, can that be fixed?
                for (int j = 0; j < memories_edible[i].positions.Count; j++)
                {
                    if( memories_edible[i].positions[j] == _go.transform.position)
                    {
                        //Do nothing if you already know the location and name of the item
                        return;
                    }
                }
                //Add position to memory if it is not already known
                memories_edible[i].positions.Add(_go.transform.position);
                return;
			}
		}
        //Create memory if it does not exist
        EdibleEffects ef;
        ef.name = _go.name;
        ef.positions = new List<Vector3>();
        ef.positions.Add(_go.transform.position);
        //Only for inital impression, increase with the actual values when eaten at a later point
        ef.effects = _go.GetComponent<Edible>().Sense();
        memories_edible.Add(ef);
	}
	
	public Vector3 FindEdible(GameObject _go)
	{
		EdibleEffects TempEffects = memories_edible[0];
		
		// Idea: somehow turning the different values (pain, poison, tasty and hunger) into something akin to the FGH values.
		// However, I wouldn't like to do this just out of the blue, besides I need sleep...
		
		for (int i = 0; i < memories_edible.Count; i++)
		{
			if (memories_edible[i].GetValue() > TempEffects.GetValue())
			{
				TempEffects = memories_edible[i];
			}
		}
		
		return TempEffects.positions[0];
	}
}