using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

	Vector3 velocity = new Vector3(0,-1,0) ;
	Vector3 desiredVelocity;
	float maxSpeed = 1.0f;
	
   	


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame

	void Update () {



	}


	void FixedUpdate(){
		
		var cubeObj = GameObject.Find("Cube");
		Debug.Log("y = " + transform.position.y);
		
		if(transform.position.y <= (cubeObj.transform.position.y + 1)){
			velocity.y = velocity.y * -1;
		}


	}

} 
