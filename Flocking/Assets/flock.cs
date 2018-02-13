using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour {
	
	public float velocity;
	
	float rotationSpeed = 4.0f;
    float neighbourDistance = 5.0f;
	bool turning = false;
    float gSpeed;
    float dist;
    int groupSize;

    Vector3 averageHeading;
    Vector3 averagePosition;
    Vector3 direction;

    Vector3 goalPos;
    Vector3 vcentre;
    Vector3 vavoid;

    GameObject[] gObj;
    


	// Use this for initialization
	void Start () {
        //start them all with a diffrent velocity
		velocity = Random.Range(1f, velocity);
	}
	
	// Update is called once per frame
	void Update () {

        //if they are about to hit the edge of the space, turn them about
		if (Vector3.Distance(transform.position, Vector3.zero) >= globalFlock.space)
            turning = true;
        else			
            turning = false;
	
        if (turning){

            //turn them around and change velocity
            direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            velocity = Random.Range(1f, velocity);

        }
        else{
           
			if (Random.Range(0,3) < 1)
				reDirect();
			

		}

        //moves everything depenat on velocity
        transform.Translate(0, 0, Time.deltaTime * velocity);
	}



	 void reDirect() {
        
        gObj = globalFlock.allBoids;
        vavoid = Vector3.zero;
        vcentre = Vector3.zero;
        goalPos = globalFlock.centerSpace;
        groupSize = 0;
        gSpeed = 0.1f;


        //checks all the other boids
        foreach (GameObject go in gObj){

            //for other boids check the how far away they are
            if (go != this.gameObject){
                dist = Vector3.Distance(go.transform.position, this.transform.position);

                //if they are close enough group them up
                if (dist <= neighbourDistance){

                    vcentre += go.transform.position;
                    groupSize++;

                    //if they are too close then avoid
                    if (dist < 1.5f)
                        vavoid = vavoid + (this.transform.position - go.transform.position);

                    //creates another flock
                    flock anotherFlock = go.GetComponent<flock>();
                    gSpeed = gSpeed + anotherFlock.velocity;

                }

            }

        }


        //if there is a group formed then s
        if (groupSize > 0){
        
          //  Debug.Log("group size : "+ groupSize);
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            //creates an avrage speed of group
            velocity = gSpeed / groupSize;

            //moves direction
            direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
			
        }

    }


}
