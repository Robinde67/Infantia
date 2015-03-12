using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_astar : MonoBehaviour {

	private class Node
	{
		public Node(GameObject _go, Vector3 _target)
		{
			go = _go;
			G = get_G(_go);
			H = get_H(_go, _target);
		}

		public float G = 0;
		public float H;
		public GameObject go;
		public Node parent = null;

		public float get_G(GameObject go)
		{
			if(go.tag == "Critter")
			{
				return 10.0f;
			}
			if(go.tag == "Edible")
			{
				return 10.0f;
			}
			if(go.tag == "Impassable")
			{
				return 500.0f;
			}
			if(go.tag == "Passable")
			{
				return 15.0f;
			}
			if(go.tag == "Predator")
			{
				return 50.0f;
			}
			if(go.tag == "Water")
			{
				return 25.0f;
			}
			return 0;

		}

		private float get_H(GameObject go, Vector3 _target) //Enum with distance types
		{
			//return ::sqrt(dx * dx + dy * dy);
			//return Vector3.Distance(go.transform.position, _target) * 10;
			float dx = _target.x - go.transform.position.x;
			float dy = _target.y - go.transform.position.y;
			float p = Mathf.Sqrt(dx * dx + dy * dy);
			return p * 10.0f;
			//float p = Mathf.Sqrt((go.transform.position.x - _target.x) * 2 + (go.transform.position.y - _target.y) * 2) + 10;
			//return p;
		}
	}

	public Grid_Space grid;
	public GameObject tempplane; //Solely for debugging
	public Vector3 target = new Vector3(5.0f, 5.0f);
	public float movSpeed = 1.0f;
	public bool spawnPlanes = false;

	private List<Node> openNodes = new List<Node>();
	private List<Node> closedNodes = new List<Node>();
	private List<Node> adjacentNodes = new List<Node>();
	private List<Node> pathNodes = new List<Node>();
	private int pathIndex = -1;
	private bool recalculate = false;

	// Use this for initialization
	void Start ()
	{
		InitAStar();
	}
	
	public void InitAStar()
	{

		Clear();
		CalculatePath();
	}

	void Clear()
	{
		openNodes.Clear();
		closedNodes.Clear();
		adjacentNodes.Clear();
		pathNodes.Clear();
	}

	void CalculatePath()
	{
		transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)); //Needs to change
		Node startNode = new Node(grid.test[(int)transform.position.x][(int)transform.position.y], target);
		startNode.G += 10.0f;
		openNodes.Add(startNode);

		int p = 0;
		while(openNodes.Count > 0 && p < 5000)
		{
			Node currentNode = openNodes[0];
			for(int i = 0; i < openNodes.Count; i++)
			{
				if(openNodes[i].G + openNodes[i].H < currentNode.G + currentNode.H)
				{
					currentNode = openNodes[i];
				}
				if(i == openNodes.Count - 1)
				{
					SpawnPlane(currentNode.go.transform.position, true);
					openNodes.Remove(currentNode);
				}
			}

			for(int i = 0; i < grid.test.Count; i++)
			{
				for(int j = 0; j < grid.test[i].Count; j++)
				{
					//Check if adjacent
					float dist = Vector3.Distance(grid.test[i][j].transform.position, currentNode.go.transform.position);
					if(dist < 1.5f && dist > 0.1f)
					{
						Node tempNode = new Node(grid.test[i][j], target);
						tempNode.parent = currentNode;
						//If it exists in openNodes/closedNodes and got a lower F value there, skip
						bool ignore = false;
						for(int o = 0; o < openNodes.Count; o++)
						{
							if(openNodes[o].go.transform.position == tempNode.go.transform.position)
							{
								if(openNodes[o].G + openNodes[o].H < tempNode.G + tempNode.H)
								{
									ignore = true;
								}
								break;
							}
						}
						if(ignore)
						{
							continue;
						}
						for(int o = 0; o < closedNodes.Count; o++)
						{
							if(closedNodes[o].go.transform.position == tempNode.go.transform.position)
							{
								if(closedNodes[o].G + closedNodes[o].H < tempNode.G + tempNode.H)
								{
									ignore = true;
								}
								break;
							}
						}
						if(ignore)
						{
							continue;
						}
						SpawnPlane(tempNode.go.transform.position, false);
						adjacentNodes.Add(tempNode);
					}
				}
			}

			for(int i = 0; i < adjacentNodes.Count; i++)
			{
				if((Vector3)target == adjacentNodes[i].go.transform.position)
				{
					Debug.Log ("Target found");
					pathNodes.Add(adjacentNodes[i]);
					Node tempNode = adjacentNodes[i];
					while(tempNode.parent != null)
					{
						pathNodes.Add(tempNode.parent);
						tempNode = tempNode.parent;
					}
					pathIndex = pathNodes.Count - 1;
					MoveToTarget();
					return;
				}
				adjacentNodes[i].G = adjacentNodes[i].parent.G + (Vector3.Distance(adjacentNodes[i].go.transform.position, adjacentNodes[i].parent.go.transform.position)
				                                                  + adjacentNodes[i].get_G(adjacentNodes[i].go) + 10.0f);
				openNodes.Add(adjacentNodes[i]);
			}
			adjacentNodes.Clear();
			closedNodes.Add(currentNode);
			p++;
		}
		Debug.Log ("Could not find a path, laps = " + p);
	}

	void MoveToTarget()
	{
		//Check if movement is done, else cancel if commanded
		if(!recalculate)
		{
			Vector3 newPos = pathNodes[pathIndex].go.transform.position;
			Debug.Log (string.Format("G: {0}, H: {1}, tag: {2}", pathNodes[pathIndex].G, pathNodes[pathIndex].H, pathNodes[pathIndex].go.tag));
			//Check with "onUpdate" if it should stop or not
			iTween.MoveTo(gameObject, iTween.Hash("x", newPos.x, "y", newPos.y, "easeType", "linear", "speed", movSpeed * Time.deltaTime * 100.0f, 
				                                      "onComplete", "NextNode", "onUpdate", "MoveUpdate"));
		}
		else
		{
			MoveUpdate();
		}
	}
	
	void NextNode()
	{
		if(pathIndex > 0)
		{
			pathIndex--;
			MoveToTarget();
		}
		else
		{
			pathIndex = -1;
		}
	}

	void MoveUpdate()
	{
		if(recalculate)
		{
			recalculate = false;
			InitAStar();
		}
	}

	public void Recalculate()
	{
		recalculate = true;
	}

	void SpawnPlane(Vector3 pos, bool green)
	{
		if(spawnPlanes)
		{
			GameObject plane = Instantiate(tempplane, pos, transform.rotation) as GameObject;
			//Physics.IgnoreCollision(GetComponent<Collider>(), plane.GetComponent<Collider>());

			if(!green)
			{
				plane.GetComponent<Renderer>().material.color = Color.red;
			}
		}
	}
}
