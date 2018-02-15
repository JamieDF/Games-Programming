using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;

public class Grid : MonoBehaviour {


	//layer for walls for unity visualation
	public LayerMask wall;

	//Grid for nodes
	Node[,] grid;
	public Vector2 gridSize;
	int gridX,gridY;

	//specfic node size	
	public float nodeRadius;
	float nodeDiameter;

	Vector3 gridCorner;
	Vector3 worldPoint;


	void Start()
	{
		//Create a grid at the right size for the nodes
		nodeDiameter = nodeRadius*2;
		gridX = Mathf.RoundToInt(gridSize.x/nodeDiameter);
		gridY = Mathf.RoundToInt(gridSize.y/nodeDiameter);
		
		
		grid = new Node[gridX,gridY];
	}


	void Update()
	{
		
		//starts at the corner of the grid then moves throgugh the rest of the of the grid to create nodes
		gridCorner = transform.position - Vector3.right * gridSize.x/2 - Vector3.forward * gridSize.y/2;

		for(int x = 0; x < gridX; x++){
			for(int y = 0; y < gridY; y++){

				//loops throught the nodes and checks if they are walkable or not
				//then creates a node at that point in the grid
				worldPoint = gridCorner + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				grid[x,y] = new Node(!(Physics.CheckSphere(worldPoint,nodeRadius,wall)),worldPoint,x,y);
			}
		}
	}

	

	public List<Node> GetNeighbour(Node node)
	{
		//will have a list of node neihbours
		List<Node> nodeNeighbours = new List<Node>();

		//loops through left and right nodes, skips over the cuernt node and then adds nodes to the list.  
		for(int x = -1; x <= 1; x++){
			for(int y = -1; y <= 1; y++){
				if(x == 0 && y == 0){
					continue;
				}

				int checkX = node.Xlocation + x;
				int checkY = node.Ylocation + y;

				if(checkX >= 0 && checkX < gridX && checkY >= 0 && checkY < gridY){
					nodeNeighbours.Add(grid[checkX,checkY]);
				}

			}
		}

		return nodeNeighbours;

	}


	
	public Node WorldPointNode(Vector3 worldPosition)
	{
			//returns the start and end point in the grid


		float xPercentage = (worldPosition.x + gridSize.x/2) / gridSize.x;
		float yPercentage = (worldPosition.z + gridSize.y/2) / gridSize.y;
		//makes whole number 
		xPercentage = Mathf.Clamp01(xPercentage);
		yPercentage = Mathf.Clamp01(yPercentage);

		return grid[Mathf.RoundToInt((gridX-1) * xPercentage),Mathf.RoundToInt((gridY-1) * yPercentage)];
	}



	public List<Node> path;


	//Unity visualiation tool
	void OnDrawGizmos()
	{	
		Gizmos.DrawWireCube(transform.position,new Vector3(gridSize.x,1,gridSize.y));


		if (gridSize != null){

			foreach (Node n in grid){
				Gizmos.color = (n.notwall)?Color.black:Color.red;
					if(path != null){
						if(path.Contains(n)){
							Gizmos.color = Color.green;
					}
				}
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
			}
		}

	}

}
