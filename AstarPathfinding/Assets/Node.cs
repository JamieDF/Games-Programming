using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;



public class Node {


	public Vector3 worldPosition;

	//keeps track of own poisition 
	public int gCost;
	public int hCost;
	public int Xlocation;
	public int Ylocation;

	//whether it can is walkable or not
	public bool notwall;

	//keeps track of its parent node
	public Node parent;
	

	public Node(bool _notwall, Vector3 _worldPosition, int _Xlocation, int _Ylocation)
	{
		
		notwall = _notwall;

		//sets nodes postions
		worldPosition = _worldPosition;
		Xlocation = _Xlocation;
		Ylocation = _Ylocation;

	}

	//cost to goal from start
	public int fCost
	{
		
		get{
			return gCost + hCost;
		}
	}

}


