using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;

public class Grid : MonoBehaviour {


	// Used to define the areas that can not be walked on.
	public LayerMask cantWalk;
	// Used to define how much space each node covers. 
	public float nodeRadius;
	// Coordinates that the grid covers.
	public Vector2 gridSize;
	// 2D array of nodes to populate the grid.
	Node[,] grid;

	//Used for the diameter of the nodes.
	float nodeDiameter;
	//Used for grid x size and grid y size.
	int gridSizeX,gridSizeY;

	// At the start of the program, check the gridSize x and y to determine how many nodes can fit
	// on the grid. Call CreateGrid();
	void Start()
	{
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridSize.y/nodeDiameter);
		CreateGrid();
	}
	void Update()
	{
		UpdateGrid();
	}
	void CreateGrid()
	{
		//Create a grid of new nodes.
		grid = new Node[gridSizeX,gridSizeY];

		//gridBottomLeft equals the bottom left corner of the grid. transform.position is the center of the grid.
		Vector3 gridBottomLeft = transform.position - Vector3.right * gridSize.x/2 - Vector3.forward * gridSize.y/2;

		//Loop through the grid to check for areas that can not be walked on. Increments by the node diameter.
		for(int x = 0; x < gridSizeX; x++)
		{
			for(int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = gridBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) 
													+ Vector3.forward * (y * nodeDiameter + nodeRadius);
													
				bool walk = !(Physics.CheckSphere(worldPoint,nodeRadius,cantWalk));
				grid[x,y] = new Node(walk,worldPoint,x,y);
			}
		}
	}
	
	void UpdateGrid()
	{
		//gridBottomLeft equals the bottom left corner of the grid. transform.position is the center of the grid.
		Vector3 gridBottomLeft = transform.position - Vector3.right * gridSize.x/2 - Vector3.forward * gridSize.y/2;

		//Loop through the grid to check for areas that can not be walked on. Increments by the node diameter.
		for(int x = 0; x < gridSizeX; x++)
		{
			for(int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = gridBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) 
													+ Vector3.forward * (y * nodeDiameter + nodeRadius);
													
				bool walk = !(Physics.CheckSphere(worldPoint,nodeRadius,cantWalk));
				grid[x,y] = new Node(walk,worldPoint,x,y);
			}
		}
	}
	// Method used to generate a list of nodes neighbouring the current node.
	public List<Node> GetNeighbouringNodes(Node node)
	{
		//New list of empty nodes.
		List<Node> nodeNeighbours = new List<Node>();

		for(int x = -1; x <= 1; x++)
		{
			for(int y = -1; y <= 1; y++)
			{
				if(x == 0 && y == 0)
				{
					continue;
				}

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					nodeNeighbours.Add(grid[checkX,checkY]);
				}

			}
		}

		return nodeNeighbours;

	}

	/*
		ADD COMMENTS FOR WORLDPOINTNODE
	
	 */
	public Node WorldPointNode(Vector3 worldPosition)
	{
		float xPercentage = (worldPosition.x + gridSize.x/2) / gridSize.x;
		float yPercentage = (worldPosition.z + gridSize.y/2) / gridSize.y;
		xPercentage = Mathf.Clamp01(xPercentage);
		yPercentage = Mathf.Clamp01(yPercentage);

		int x = Mathf.RoundToInt((gridSizeX-1) * xPercentage);
		int y = Mathf.RoundToInt((gridSizeY-1) * yPercentage);

		//return the current nodes x,y position in the grid.
		return grid[x,y];

	}

	public List<Node> path;


	// This method is used to visualise the grid on the screen.(built-in unity function)
	void OnDrawGizmos()
	{	
		Gizmos.DrawWireCube(transform.position,new Vector3(gridSize.x,1,gridSize.y));

		// Test to check that the grid is populated with nodes.
		if (gridSize != null)
		{

			foreach (Node n in grid)
			{
				Gizmos.color = (n.walk)?Color.white:Color.red;
					if(path != null){
						if(path.Contains(n)){
							Gizmos.color = Color.black;
					}
				}
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
			}
		}

	}

}
