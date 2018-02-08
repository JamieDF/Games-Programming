using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;

public class Node {

	public bool walk;
	public Vector3 worldPosition;

	public int gCost;
	public int hCost;
	public int gridX;
	public int gridY;

	public Node parent;
	

	public Node(bool _walk, Vector3 _worldPosition, int _gridX, int _gridY)
	{
		walk = _walk;
		worldPosition = _worldPosition;
		gridX = _gridX;
		gridY = _gridY;

	}
	public int fCost
	{
		get{
			return gCost + hCost;
		}
	}

}


