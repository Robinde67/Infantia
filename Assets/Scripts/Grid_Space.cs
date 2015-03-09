using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid_Space : MonoBehaviour {

	public int x, y;
	public float size;

	public GameObject Grid_Container;
	public GameObject Background;

    public int spawn_frequency = 5; //Lower is more frequent
    public int frequency_critters; //Higher is more frequent
    public int frequency_predators;
    public int frequency_edible;
    public int frequency_impassable;
    public int frequency_passable;
    public int frequency_water;

	public List<List<GameObject>> test = new List<List<GameObject>>();
	private List<GameObject> Critters = new List<GameObject>();
    private List<GameObject> Predators = new List<GameObject>();
    private List<GameObject> Edible = new List<GameObject>();
    private List<GameObject> Impassable = new List<GameObject>();
    private List<GameObject> Passable = new List<GameObject>();
    private List<GameObject> Water = new List<GameObject>();

	//public GameObject[,] grid;

	// Use this for initialization
	void Start () {

        Object[] SubList_Critters = Resources.LoadAll("Prefabs/World Objects/Critters", typeof(GameObject));
        Object[] SubList_Predators = Resources.LoadAll("Prefabs/World Objects/Predators", typeof(GameObject));
        Object[] SubList_Edible = Resources.LoadAll("Prefabs/World Objects/Edible", typeof(GameObject));
        Object[] SubList_Impassable = Resources.LoadAll("Prefabs/World Objects/Impassable", typeof(GameObject));
        Object[] SubList_Passable = Resources.LoadAll("Prefabs/World Objects/Passable", typeof(GameObject));
        Object[] SubList_Water = Resources.LoadAll("Prefabs/World Objects/Water", typeof(GameObject));

        foreach (GameObject SubList_Object in SubList_Critters) 
        {    
            GameObject lo = (GameObject)SubList_Object;
            Critters.Add(lo);
        }
        foreach (GameObject SubList_Object in SubList_Predators) 
        {    
            GameObject lo = (GameObject)SubList_Object;
            Predators.Add(lo);
        }
        foreach (GameObject SubList_Object in SubList_Edible) 
        {    
            GameObject lo = (GameObject)SubList_Object;
            Edible.Add(lo);
        }
        foreach (GameObject SubList_Object in SubList_Impassable) 
        {    
            GameObject lo = (GameObject)SubList_Object;
            Impassable.Add(lo);
        }
        foreach (GameObject SubList_Object in SubList_Passable) 
        {    
            GameObject lo = (GameObject)SubList_Object;
            Passable.Add(lo);
        }
        foreach (GameObject SubList_Object in SubList_Water) 
        {    
            GameObject lo = (GameObject)SubList_Object;
            Water.Add(lo);
        }

		for(int i = 0; i < x; i++)
		{
			test.Add(new List<GameObject>());
			for(int j = 0; j < y; j++)
			{
				GameObject bg = Instantiate(Background, new Vector3(i * size, j * size), Background.transform.rotation) as GameObject;
				bg.transform.localScale = bg.transform.localScale * 0.1f;

				if(Random.Range(0, 0) == 0)
				{
                    GameObject cur_prefab = Edible[0];
                    int p = frequency_critters + frequency_edible + frequency_impassable + frequency_passable + frequency_predators + frequency_water;
                    int q = 0;
                    int r = Random.Range(0, p);
                    bool found = false;
                    q += frequency_critters;
                    if(r < q && !found && Critters.Count > 0)
                    {
                        cur_prefab = Critters[Random.Range (0, Critters.Count)];
                        found = true;
                    }
                    q += frequency_edible;
                    if(r < q && !found && Edible.Count > 0)
                    {
                        cur_prefab = Edible[Random.Range (0, Edible.Count)];
                        found = true;
                    }
                    q += frequency_impassable;
                    if(r < q && !found && Impassable.Count > 0)
                    {
                        cur_prefab = Impassable[Random.Range (0, Impassable.Count)];
                        found = true;
                    }
                    q += frequency_passable;
                    if(r < q && !found && Passable.Count > 0)
                    {
                        cur_prefab = Passable[Random.Range (0, Passable.Count)];
                        found = true;
                    }
                    q += frequency_predators;
                    if(r < q && !found && Predators.Count > 0)
                    {
                        cur_prefab = Predators[Random.Range (0, Predators.Count)];
                        found = true;
                    }
                    q += frequency_water;
                    if(r < q && !found && Water.Count > 0)
                    {
                        cur_prefab = Water[Random.Range (0, Water.Count)];
                        found = true;
                    }

                    //if(found){
						GameObject go = Instantiate(cur_prefab, new Vector3(i * size, j * size), transform.rotation) as GameObject;
						go.transform.parent = Grid_Container.transform;
						test[i].Add(go);
                    //}
				}
			}
		}
	}
}
