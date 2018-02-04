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
		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
		transform.position = transform.position + velocity; 
		Debug.Log("y = " + transform.position.y);
		if(transform.position.y <= 0){
			Debug.Log("y = 0");
			velocity.y = velocity.y * velocity.y;
		}

	}

} 
