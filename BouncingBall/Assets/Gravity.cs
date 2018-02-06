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

	
	bool moving;
	bool decent;
	int bounce = 0;





	// Use this for initialization
	void Start () 
	{

		floor =  GameObject.Find("Cube");
		Collider = GameObject.Find("sphereCollider");

		moving = true;
		decent = true;
	}
	
	void Update () {
		
	}


	void FixedUpdate(){
		
		//if ball is in motion
		if(moving){


			//ensures the ball only bounces on decent
			if(velocity.y <= 0){
				decent = true;
			}
			else
				decent = false;
			

			//if ball hits the floor cube	
			if((Collider.transform.position.y-2.5) <= (floor.transform.position.y) && decent){
				bounce++;
				Debug.Log(bounce);

				//engery is lost(realisticly to sound and heat)
				velocity.y = (velocity.y * -1) * engeryLoss;

				//ball stopes bouncing
				if(bounce == 20){
					moving = false;
				}
				
				
			}

			//acceleration and deceleration due to grabity
			velocity = velocity + gravity * Time.fixedDeltaTime;
			//ball and colider movement based on new velocity
			transform.Translate(velocity * Time.fixedDeltaTime);
			Collider.transform.position = transform.position;
			
		}

		


		


	}
}
