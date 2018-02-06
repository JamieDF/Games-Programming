using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {



	Vector3 velocity = new Vector3(0, 0, 0);
	Vector3 acceleration;
	Vector3 rotation;
	Vector3 gravity = new Vector3(0, -9.8f, 0);
	Vector3 force;
	float mass = 20.0f;
	float maxSpeed = 1.0f;
	bool moving = true;
	bool decent = true;
	float engeryLoss = 0.8f;
	GameObject floor;
	GameObject Collider;
	int bounce = 0;
	// Use this for initialization
	void Start () 
	{
		force = gravity * mass;
		moving = true;
		floor =  GameObject.Find("Cube");
		Collider = GameObject.Find("sphereCollider");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void FixedUpdate(){
		
		
		//Debug.Log(velocity.y);
		if(moving){

			if(velocity.y <= 0){
				decent = true;
			}
			else
				decent = false;
			
				
			if((Collider.transform.position.y-2.5) <= (floor.transform.position.y) && decent){
				bounce++;
				Debug.Log(bounce);
				velocity.y = (velocity.y * -1) * engeryLoss;
				if(bounce == 20){
					moving = false;
				}
				
				
			}



			velocity = velocity + gravity * Time.fixedDeltaTime;
				
			//Debug.Log("ball :"+ velocity);
			transform.Translate(velocity * Time.fixedDeltaTime);
			//Debug.Log("colider :"+ velocity);
			Collider.transform.position =transform.position;
			//Debug.Log(velocity);
		}

		


		


	}
}
