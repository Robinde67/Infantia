using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid_Space : MonoBehaviour {

	public int x, y;
	public float size;

	public GameObject Grid_Container;
	public GameObject Background;

	public List<List<GameObject>> test = new List<List<GameObject>>();
	private List<GameObject> Objects = new List<GameObject>();

	//public GameObject[,] grid;

	// Use this for initialization
	void Start () {

		Object[] SubList_Objects = Resources.LoadAll("Prefabs/World Objects", typeof(GameObject));
		foreach (GameObject SubList_Object in SubList_Objects) 
		{    
			GameObject lo = (GameObject)SubList_Object;
			Objects.Add(lo);
		}

		for(int i = 0; i < x; i++)
		{
			test.Add(new List<GameObject>());
			for(int j = 0; j < y; j++)
			{
				if(Objects.Count > 0)
				{
					GameObject bg = Instantiate(Background, new Vector3(i * size, j * size), Background.transform.rotation) as GameObject;
					bg.transform.localScale = bg.transform.localScale * 0.1f;

					if(Random.Range(0, 5) == 0)
					{
						GameObject go = Instantiate(Objects[Random.Range (0, Objects.Count)], new Vector3(i * size, j * size), transform.rotation) as GameObject;
						go.transform.parent = Grid_Container.transform;
						test[i].Add(go);
					}
				}
				else
				{
					Debug.LogError("Found no objects to place");
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
