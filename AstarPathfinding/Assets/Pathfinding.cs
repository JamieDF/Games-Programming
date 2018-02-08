using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;

public class Pathfinding : MonoBehaviour {

	Grid grid;
	public Transform seeker,target;

	void Awake()
	{
		grid = GetComponent<Grid>();
	}

	void Update()
	{
		FindPath(seeker.position,target.position);
		
	}

	void FindPath(Vector3 startingPosition, Vector3 targetPosition)
	{
		Node startingNode = grid.WorldPointNode(startingPosition);
		Node targetNode = grid.WorldPointNode(targetPosition);

		List<Node> open = new List<Node>();
		HashSet<Node> closed = new HashSet<Node>();

		open.Add(startingNode);

		while(open.Count > 0)
		{
			Node currentNode = open[0];

			for(int i = 1; i < open.Count; i++)
			{
				if(open[i].fCost < currentNode.fCost || open[i].fCost == currentNode.fCost && open[i].hCost < currentNode.hCost)
				{
					currentNode = open[i];
				}
			}

			open.Remove(currentNode);
			closed.Add(currentNode);

			if (currentNode == targetNode)
			{	
				RetracePath(startingNode, targetNode);
				return;
			}

			foreach(Node neighbour in grid.GetNeighbouringNodes(currentNode))
			{
				if(!neighbour.walk || closed.Contains(neighbour))
				{
					continue;
				}

				int newMovementCostToNeighbour = currentNode.gCost + Distance(currentNode, neighbour);
				if (newMovementCostToNeighbour < neighbour.gCost || !open.Contains(neighbour))
				{
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = Distance(neighbour, targetNode);
					neighbour.parent = currentNode;

					if (!open.Contains(neighbour))
					{
						open.Add(neighbour);
					}
						
				}
			}
		}
	}

	void RetracePath(Node startNode, Node endNode)
	{
		List<Node> Paths = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode){
			Paths.Add(currentNode);
			currentNode = currentNode.parent;

		}
		Paths.Reverse();
	
		grid.path = Paths;
	}


	int Distance (Node nodeA, Node nodeB)
	{
		int distX = Mathf.Abs(nodeA.gridX -nodeB.gridX);
		int distY = Mathf.Abs(nodeA.gridY -nodeB.gridY);


		if (distX > distY)
		{
			return 14*distY + 10* (distX-distY);
		}

		else
		{
			return 14*distX + 10* (distY-distX);
		}



	}

}
