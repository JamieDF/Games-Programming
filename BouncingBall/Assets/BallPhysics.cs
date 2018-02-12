using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour {

	//objects floor and sphere colider
	GameObject floor;
	GameObject Collider;

	//gravity velocity and the energy loss from hitting the floor
	Vector3 velocity = new Vector3(0, 0, 0);
	Vector3 gravity = new Vector3(0, -9.8f, 0);
	float engeryLoss = 0.8f;

	//check if the ball is falling, going down and is bouncing
	bool motion;
	int bounce = 0;

	// Use this for initialization
	void Start () 
	{

		floor =  GameObject.Find("Cube");
		Collider = GameObject.Find("sphereCollider");

		//begin the ball falling 
		motion = true;
	}


	void FixedUpdate(){

		//the ball is moving 		
		if(motion){

			//if collider touches floor and has a negitve velocity
			if((Collider.transform.position.y-2.5) <= (floor.transform.position.y) && velocity.y <=0){
				bounceBall();
			}

			moveObj();
		}
	}


	void bounceBall(){

		bounce++;
		Debug.Log(bounce);

		//engery is lost(realisticly to sound and heat)
		velocity.y = (velocity.y * -1) * engeryLoss;

		//after a time bouncing, stop the ball
		if(bounce == 20){
			motion = false;
		}		
	}


	void moveObj(){
		//velocity accelerated/slow by gravity
		velocity = velocity + gravity * Time.fixedDeltaTime;
		//move the ball and its collider
		transform.Translate(velocity * Time.fixedDeltaTime);
		Collider.transform.position = transform.position;

	}
}
