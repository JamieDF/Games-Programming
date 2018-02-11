using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

	//objects floor and sphere col;ider
	GameObject floor;
	GameObject Collider;

	//physics and movemnt
	Vector3 velocity = new Vector3(0, 0, 0);
	Vector3 gravity = new Vector3(0, -9.8f, 0);
	float engeryLoss = 0.8f;

	
	bool fall;
	bool motion;
	int bounce = 0;

	// Use this for initialization
	void Start () 
	{

		floor =  GameObject.Find("Cube");
		Collider = GameObject.Find("sphereCollider");

		fall = true;
		motion = true;
	}

	void FixedUpdate(){
		
		
		if(motion){
			if(velocity.y <= 0)
				fall = true;

			else
				fall = false;
			
			if((Collider.transform.position.y-2.5) <= (floor.transform.position.y) && fall){
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

		//ball stopes bouncing
		if(bounce == 20){
			motion = false;
		}		
	}


	void moveObj(){

		velocity = velocity + gravity * Time.fixedDeltaTime;
		transform.Translate(velocity * Time.fixedDeltaTime);
		Collider.transform.position = transform.position;

	}
}
