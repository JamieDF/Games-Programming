using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;

public class Pathfinding : MonoBehaviour {

	Grid grid;
	public Transform seeker,target;
	Node startingNode;
	Node targetNode;


	void Start()
	{	
		//loads the grid
		grid = GetComponent<Grid>();
	}

	void Update()
	{
		//seeker
		startingNode = grid.WorldPointNode(seeker.position);
		//target
		targetNode = grid.WorldPointNode(target.position);

		//sorts the nodes into checked and unchecked
		List<Node> open = new List<Node>();
		List<Node> closed = new List<Node>();

		//add the seeker node to open
		open.Add(startingNode);


		//loops through all nodes 
		while(open.Count > 0){
			Node currentNode = open[0];

			//Compares cost of open nodes and if there is a better option then add it.
			for(int i = 1; i < open.Count; i++){
				if(open[i].fCost < currentNode.fCost || open[i].fCost == currentNode.fCost && open[i].hCost < currentNode.hCost){
					currentNode = open[i];
				}
			}

			//remove that node from the open and add it to the close to show its been checked
			open.Remove(currentNode);
			closed.Add(currentNode);

			//if the target is found the call retrace path
			if (currentNode == targetNode){	
				RetracePath(startingNode, targetNode);
				return;
			}

			//searches the nabouors that are left, i.e the ones that are walkable and that are not closed
			foreach(Node neighbour in grid.GetNeighbour(currentNode)){
				if(!neighbour.notwall || closed.Contains(neighbour)){
					continue;
				}

				//gets the cost using the ecludian huristic
				int newMovementCostToNeighbour = currentNode.gCost + Distance(currentNode, neighbour);
				//if the cost is smaller that the other ones then choose that to be the best node for the path
				if (newMovementCostToNeighbour < neighbour.gCost || !open.Contains(neighbour)){
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = Distance(neighbour, targetNode);
					neighbour.parent = currentNode;


					//if its not been added alread then add it 
					if (!open.Contains(neighbour))
						open.Add(neighbour);
					
						
				}
			}
		}
		
	}

	//create path List then, loops throuhg the nodes backwards and draws the path in reverse
	void RetracePath(Node startNode, Node endNode)
	{

		List<Node> Paths = new List<Node>();
		Node currentNode = endNode;

		//loop through the path untill the get to the start node
		while (currentNode != startNode){
			Paths.Add(currentNode);
			currentNode = currentNode.parent;
			//uses the parent to find the previous node to find the path

		}
		Paths.Reverse();
	
		grid.path = Paths;
	}



	int Distance (Node nodeA, Node nodeB)
	{

		//heuristic is used here to in distance calucation.
		//a diagonal move is worth 2 and an staight movemnt is 1


		int Xdistance = Mathf.Abs(nodeA.Xlocation -nodeB.Xlocation);
		int ydistance = Mathf.Abs(nodeA.Ylocation -nodeB.Ylocation);
		int diagonal = 2;
		int straight = 1;

		if (Xdistance > ydistance)
			return diagonal*ydistance + straight* (Xdistance-ydistance);
		
		else
			return diagonal*Xdistance + straight* (ydistance-Xdistance);


		//In place of using the vector3 GetDistance which uses euclidean heuristics,  This function uses the manhattan distanace

	}

}
