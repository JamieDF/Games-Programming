using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour {
	
	public float velocity;
	
	float rotationSpeed = 4.0f;

    Vector3 averageHeading;
    Vector3 averagePosition;

    float neighbourDistance = 5.0f;


	bool turning = false;


	// Use this for initialization
	void Start () {
		velocity = Random.Range(0.5f, 1);
	}
	
	// Update is called once per frame
	void Update () {


		if (Vector3.Distance(transform.position, Vector3.zero) >= globalFlock.space){

            turning = true;
        }

        else{
			
            turning = false;
		}

        if (turning){

            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            velocity = Random.Range(0.5f, 1);

        }
        else{

			if (Random.Range(0, 5) < 1){
				ApplyLaws();
			}

		}

        transform.Translate(0, 0, Time.deltaTime * velocity);
	}


	 void ApplyLaws() {

        GameObject[] gObj;
        gObj = globalFlock.allBoids;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;

        Vector3 goalPos = globalFlock.centerSpace;
        float dist;

        int groupSize = 0;

        foreach (GameObject go in gObj){

            if (go != this.gameObject){

                dist = Vector3.Distance(go.transform.position, this.transform.position);

                if (dist <= neighbourDistance){

                    vcentre += go.transform.position;
                    groupSize++;

                    if (dist < 1.0f){
                        vavoid = vavoid + (this.transform.position - go.transform.position);

                    }

                    flock anotherFlock = go.GetComponent<flock>();
                    gSpeed = gSpeed + anotherFlock.velocity;

                }

            }

        }


        if (groupSize > 0){

            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            velocity = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero){
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
			}
        }

    }


}
