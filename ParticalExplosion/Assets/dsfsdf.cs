// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class dsfsdf : MonoBehaviour {

// 	public GameObject Target;
// 	Vector3 velocity = new Vector3(0,0,1) ;
// 	Vector3 desiredVelocity;
// 	float maxSpeed = 1.0f;
// 	float maxForce = 1.0f;
// 	float mass = 2.0f;

// 	// Use this for initialization
// 	void Start () {
		
		
// 		;
		
// 	}
	
// 	// Update is called once per frame
// 	void Update () {
// 		desiredVelocity = Vector3.ClampMagnitude(Target.transform.position - transform.position, velocity.magnitude);
// 		Vector3 steering = Vector3.ClampMagnitude(desiredVelocity - velocity, maxForce);
// 		steering = steering/mass;
// 		velocity = Vector3.ClampMagnitude(velocity + steering, maxSpeed);
// 		transform.position = transform.position + velocity;
// 		// transform.Translate(velocity + Time.deltaTime);
// 		// transform.Translate(velocity + Time.deltaTime, Space.World);
	
// 	}
// }
