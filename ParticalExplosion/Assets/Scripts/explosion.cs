using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour {

	//physics and movemnt
	Vector3 cubevelocity;
	Vector3 velocity;
	Vector3 gravity = new Vector3(0, -9.8f, 0);

	public static int numSelectors = 50;
	public static string selector = "star_prefab";
	public GameObject[] selectorArr;
 	public bool expload;
	 public bool falling;

	Vector3[] velocityArr = new Vector3[numSelectors];		 
	GameObject particleObject;
	public int deteriorate;

	public GameObject cube;



	// Use this for initialization
	void Start () 
	{
		cube = Instantiate(Resources.Load ("cube")) as GameObject;
		deteriorate = 0;
		selectorArr = new GameObject[numSelectors];
        for (int i = 0; i < numSelectors; i++)
        {
            particleObject = Instantiate(Resources.Load (selector)) as GameObject;
            particleObject.transform.localScale = Vector3.one;
			particleObject.transform.position = new Vector3(0,0,0);

			selectorArr[i] = particleObject;
			
			velocityArr[i] = new Vector3(Random.Range(-10.0f, 20.0f),Random.Range(-10.0f, 20.0f),Random.Range(-10.0f, 20.0f));
			selectorArr[i].SetActive(false);
            
        }
	}
	
	
	void Update () {
		
	}


	void FixedUpdate(){
		if (Input.GetKeyDown("space")){
				print("space key was pressed");
				falling = true;
		}

		if(falling){
			if(cube.transform.position.y <=0){
				falling = false;
				expload =true;
				cube.SetActive(false);
			}
			else{
				cubevelocity = cubevelocity + gravity * Time.fixedDeltaTime;
				//ball and colider movement based on new velocity
				cube.transform.Translate(cubevelocity * Time.fixedDeltaTime);

			}
		}
		//if ball is in motion
		if (expload){
			deteriorate++;
			for(int i=0; i < numSelectors; i++){
				selectorArr[i].SetActive(true);
									
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