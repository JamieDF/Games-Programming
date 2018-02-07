using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	//physics and movemnt
	Vector3 velocity;
	Vector3 gravity = new Vector3(0, -9.8f, 0);

	static int numSelectors = 75;
	public GameObject[] selectorArr;
 	public bool expload;

	Vector3[] velocityArr = new Vector3[numSelectors];		 
	GameObject particleObject;
	public int deteriorate;



	// Use this for initialization
	void Start () 
	{
		deteriorate = 0;
		selectorArr = new GameObject[numSelectors];
        for (int i = 0; i < numSelectors; i++)
        {
            particleObject = Instantiate(Resources.Load ("particle")) as GameObject;
            particleObject.transform.localScale = Vector3.one;
			particleObject.transform.position = new Vector3(0,0,0);

			selectorArr[i] = particleObject;
			
			velocityArr[i] = new Vector3(Random.Range(-5.0f, 5.0f),Random.Range(-5.0f, 5.0f),Random.Range(-5.0f, 5.0f));
			
            
        }
	}
	
	
	void Update () {
		
	}


	void FixedUpdate(){
		if (Input.GetKeyDown("space")){
				print("space key was pressed");
				expload = true;
		}

		
		//if ball is in motion
		if (expload){
			deteriorate++;
			for(int i=0; i < numSelectors; i++){
				
									
					velocityArr[i] = velocityArr[i] + gravity * Time.fixedDeltaTime;
					
					//ball and colider movement based on new velocity
					selectorArr[i].transform.Translate(velocityArr[i] * Time.fixedDeltaTime);


			}
		}

		if (deteriorate == 80){
			expload = false;
			for(int i=0; i < numSelectors; i++){
				Destroy(selectorArr[i]);
			}
		}

	}
}